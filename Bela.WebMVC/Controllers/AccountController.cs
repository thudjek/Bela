using Bela.Application.Interfaces;
using Bela.Application.Utility;
using Bela.Application.ViewModels.User;
using Bela.WebMVC.Extensions;
using Bela.WebMVC.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bela.WebMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;
        public AccountController(
            IIdentityService identityService,
            IEmailService emailService)
        {
            _identityService = identityService;
            _emailService = emailService;
        }

        [ServiceFilter(typeof(RestrictToAuthorized))]
        public IActionResult ForgottenPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgottenPassword(ForgottenPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityService.GetPasswordResetResult(model.Email);
                if (result.IsSucessfull)
                {
                    var link = Url.Action("ResetPassword", "Account", result.Values[0], Request.Scheme);
                    EmailMessage message = EmailGenerator.GeneratePasswordResetMessage(link, model.Email);
                    await _emailService.SendEmailAsync(message);
                }

                return View("ForgotPasswordConfirmation");
            }

            return View();
        }

        [ServiceFilter(typeof(RestrictToAuthorized))]
        public IActionResult ResetPassword(string token, string email)
        {
            ResetPasswordModel model = new ResetPasswordModel
            {
                Token = token,
                Email = email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                await _identityService.ResetPassword(model);
                return View("ResetPasswordConfirmation");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> UserDetails(int id)
        {
            if (id == 0)
                id = User.GetUserId();

            var model = await _identityService.GetUserDetailsModelAsync(id);
            return PartialView("_UserDetailsPartial", model);
        }
    }
}
