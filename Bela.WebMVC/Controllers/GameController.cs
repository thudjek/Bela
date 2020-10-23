using Bela.Application.Interfaces;
using Bela.Application.Utility;
using Bela.Application.ViewModels.Game;
using Bela.Domain.Enums;
using Bela.WebMVC.Extensions;
using Bela.WebMVC.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bela.WebMVC.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IHubContext<GameHub> _gameHubContext;
        public GameController(
            IGameService gameService,
            IHubContext<GameHub> gameHubContext)
        {
            _gameService = gameService;
            _gameHubContext = gameHubContext;
        }

        public async Task<IActionResult> Index()
        {
            //var userId = User.GetUserId();
            //if (!await _gameService.IsPlayerInGame(userId))
            //    return RedirectToAction("Index", "Lobby");

            //var model = await _gameService.GetGameViewModelAsync(userId);
            //return View(model);

            var model = new GameViewModel()
            {
                Id = 1,
                UserNameLeft = "popay",
                UserNameRight = "miki",
                UserNameDown = "viz",
                UserNameUp = "daf",
                Position = Domain.Enums.PlayerPosition.Down
            };
            return View(model);
        }

        public IActionResult GetCardsInHandViewComponent(bool allCards, bool isPlayable)
        {
            return ViewComponent("CardsInHand", new { allCards, isPlayable });
        }

        public async Task<JsonResult> GetTotalScores(int gameId)
        {
            var userId = User.GetUserId();
            var scores = await _gameService.GetTotalScores(gameId, userId);
            return Json(scores);
        }

        public async Task<JsonResult> GetRoundScores(int roundId)
        {
            var userId = User.GetUserId();
            var scores = await _gameService.GetRoundScores(roundId, userId);
            return Json(scores);
        }

        public async Task<JsonResult> GetCurrentData(int gameId)
        {
            var userId = User.GetUserId();
            var model = await _gameService.GetCurrentRoundData(gameId, userId);
            return Json(model);
        }

        public async Task<JsonResult> SelectTrump(int roundId, int trump, string username)
        {
            var result = await _gameService.SelectTrump(roundId, trump, username);
            if (result.IsSucessfull)
            {
                var gameId = result.Values[0] as string;
                if(trump > 0)
                    await _gameHubContext.Clients.Group("Game" + gameId).SendAsync("TrumpCalled", result.Values[1]);
                else
                    await _gameHubContext.Clients.Group("Game" + gameId).SendAsync("TurnPassed", result.Values[1]);

                return Json(new { success = true });
            }
            else 
            {
                return Json(new { error = result.Errors[0] });
            }
        }

        public async Task<JsonResult> MakeACall(List<string> cardStrings, int roundId)
        {
            var userId = User.GetUserId();
            var result = await _gameService.MakeACall(cardStrings, roundId, userId);
            return Json(new { success = true });
        }
    }
}
