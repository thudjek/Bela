using Bela.Application.Interfaces;
using Bela.WebMVC.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bela.WebMVC.Controllers
{
    public class LobbyController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IGameService _gameService;
        public LobbyController(
            IIdentityService identityService,
            IGameService gameService)
        {
            _identityService = identityService;
            _gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.IsInRoom = false;
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.IsInRoom = await _identityService.GetUsersRoomId(User.GetUserId()) > 0;

                var userId = User.GetUserId();
                if (await _gameService.IsPlayerInGame(userId))
                    return RedirectToAction("Index", "Game");
            }

            return View();
        }

        public IActionResult GetRoomListViewComponent(string filterRoomName)
        {
            return ViewComponent("RoomList", filterRoomName);
        }

        public IActionResult GetUserListViewComponent(string filterUsername, bool isRoomAndOwner)
        {
            return ViewComponent("UserList", new { filterUsername, isRoomAndOwner });
        }
    }
}
