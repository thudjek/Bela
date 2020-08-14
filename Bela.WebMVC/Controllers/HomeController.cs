using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bela.Application.Interfaces;
using Bela.Application.Utility;
using Bela.Application.ViewModels.User;
using Bela.WebMVC.Extensions;
using Bela.WebMVC.Filters;
using Bela.WebMVC.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Bela.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;
        private readonly IHubContext<LobbyHub> _lobbyHubContext;
        private readonly IHubContext<RoomHub> _roomHubContext;

        public HomeController(
            IIdentityService identityService,
            IEmailService emailService,
            IHubContext<LobbyHub> lobbyHubContext,
            IHubContext<RoomHub> roomHubContext)
        {
            _identityService = identityService;
            _emailService = emailService;
            _lobbyHubContext = lobbyHubContext;
            _roomHubContext = roomHubContext;
    }

        [ServiceFilter(typeof(RestrictToAuthorized))]
        public IActionResult Index()
        {
            return View();
        }

        [ServiceFilter(typeof(RestrictToAuthorized))]
        public IActionResult Login()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityService.LogInUser(model);
                if (result.IsSucessfull)
                {
                    await _roomHubContext.Clients.All.SendAsync("UpdateUserList");
                    await _lobbyHubContext.Clients.Group("AuthGroup").SendAsync("UpdateUserList");
                    return RedirectToAction("Index", "Lobby");
                }
                else 
                {
                    ModelState.AddErrors(result.Errors);
                } 
            }
            return View("Index");
        }

        [ServiceFilter(typeof(RestrictToAuthorized))]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityService.RegisterUser(model);
                if (result.IsSucessfull)
                {
                    var link = Url.Action("ConfirmEmail", "Home", result.Values[0], Request.Scheme);
                    EmailMessage message = EmailGenerator.GenerateConfirmEmailMessage(link, model.Email);
                    await _emailService.SendEmailAsync(message);

                    ViewBag.Email = model.Email;
                    return View("EmailActivation");
                }
                else
                {
                    ModelState.AddErrors(result.Errors);
                }
            }
            return View();
        }

        [ServiceFilter(typeof(RestrictToAuthorized))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _identityService.ConfirmEmailActivation(userId, token);
            ViewBag.Success = result.IsSucessfull;
            return View("EmailConfirmation");
        }

        public async Task<IActionResult> Logout()
        {
            var userId = User.GetUserId();
            await _identityService.LogOutUser(userId);
            await _roomHubContext.Clients.All.SendAsync("UpdateUserList");
            await _lobbyHubContext.Clients.Group("AuthGroup").SendAsync("UpdateUserList");
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PageNotFound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
