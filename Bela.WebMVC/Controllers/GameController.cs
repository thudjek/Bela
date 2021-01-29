using Bela.Application.Interfaces;
using Bela.Application.Utility;
using Bela.Application.ViewModels.Game;
using Bela.Domain.Enums;
using Bela.WebMVC.Extensions;
using Bela.WebMVC.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bela.WebMVC.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IHubContext<LobbyHub> _lobbyHubContext;
        private readonly IHubContext<GameHub> _gameHubContext;
        public GameController(
            IGameService gameService,
            IHubContext<LobbyHub> lobbyHubContext,
            IHubContext<GameHub> gameHubContext)
        {
            _gameService = gameService;
            _lobbyHubContext = lobbyHubContext;
            _gameHubContext = gameHubContext;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserId();
            if (!await _gameService.IsPlayerInGame(userId))
                return RedirectToAction("Index", "Lobby");

            var model = await _gameService.GetGameViewModelAsync(userId);
            return View(model);
        }

        public IActionResult EndScreen()
        {
            return RedirectToAction("Index", "Lobby");
        }

        [HttpPost]
        public async Task<IActionResult> EndScreen(int gameId, string myUsername, string teammateUsername, string opponent1Username, string opponent2Username)
        {
            var userId = User.GetUserId();
            var model = await _gameService.GetEndScreenViewModel(gameId, myUsername, teammateUsername, opponent1Username, opponent2Username, userId);

            if (model == null)
                return RedirectToAction("Index", "Lobby");

            return View(model);
        }

        [HttpPost]
        public IActionResult EndScreenQ(string quitUsername, string opponent1Username, string opponent2Username)
        {
            var model = _gameService.GetEndScreenViewModelForPlayerQuitting(quitUsername, opponent1Username, opponent2Username);

            if (model == null)
                return RedirectToAction("Index", "Lobby");

            return View("EndScreen", model);
        }

        public IActionResult GetCardsInHandViewComponent(bool allCards, bool isPlayable, int trump)
        {
            return ViewComponent("CardsInHand", new { allCards, isPlayable, trump });
        }

        public IActionResult GetCallOnTableViewComponent(int roundId, int playerPosition, bool isPartner)
        {
            return ViewComponent("CallOnTable", new { roundId, playerPosition, isPartner });
        }

        public async Task<JsonResult> GetTotalScores(int gameId)
        {
            var userId = User.GetUserId();
            var model = await _gameService.GetTotalScoresAndRounds(gameId, userId);
            return Json(model);
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
            TimerBela timer = TimerHelper.GetTimerForGameId(Convert.ToInt32(gameId));
            model.RemainingTime = timer != null ? TimerHelper.GetTimerForGameId(Convert.ToInt32(gameId)).GetRemainingTime() : 0;
            return Json(model);
        }

        public async Task<JsonResult> SelectTrump(int roundId, int trump, string username, bool isLast)
        {
            var result = await _gameService.SelectTrump(roundId, trump, username);
            if (result.IsSucessfull)
            {
                var gameId = result.Values[0] as string;
                if (trump > 0)
                    await _gameHubContext.Clients.Group("Game" + gameId).SendAsync("TrumpCalled", result.Values[1]);
                else
                    await _gameHubContext.Clients.Group("Game" + gameId).SendAsync("TurnPassed", result.Values[1]);

                var timer = TimerHelper.GetTimerForGameId(Convert.ToInt32(gameId));
                if(timer != null)
                    timer.Restart();

                return Json(new { success = true });
            }
            else
            {
                return Json(new { error = result.Errors[0], isLast });
            }
        }

        public async Task<JsonResult> MakeACall(List<string> cardStrings, int roundId, bool isCall)
        {
            var userId = User.GetUserId();
            var result = await _gameService.MakeACall(cardStrings, roundId, userId, isCall);
            if (result.IsSucessfull)
            {
                var gameId = result.Values[0] as string;
                if (isCall)
                    await _gameHubContext.Clients.Group("Game" + gameId).SendAsync("CallMade", result.Values[1]);
                else
                    await _gameHubContext.Clients.Group("Game" + gameId).SendAsync("TurnPassed", result.Values[1]);

                var timer = TimerHelper.GetTimerForGameId(Convert.ToInt32(gameId));
                if (timer != null)
                    timer.Restart();

                return Json(new { success = true });
            }
            else
            {
                return Json(new { error = result.Errors[0] });
            }
        }

        public async Task<JsonResult> PlayACard(string cardString, int roundId, PlayerPosition position, List<string> cardsInHandStrings, bool belaCalled) 
        {
            var userId = User.GetUserId();
            var result = await _gameService.PlayACard(cardString, roundId, userId, position, cardsInHandStrings, belaCalled);
            if (result.IsSucessfull)
            {
                var gameId = result.Values[0] as string;
                var isGameOver = result.Values[2] as bool?;
                await _gameHubContext.Clients.Group("Game" + gameId).SendAsync("CardPlayed", result.Values[1]);

                if (isGameOver.Value)
                {
                    TimerHelper.RemoveTimerForGameId(Convert.ToInt32(gameId));
                    await _lobbyHubContext.Clients.All.SendAsync("UpdateRoomList");
                }
                else 
                {
                    var timer = TimerHelper.GetTimerForGameId(Convert.ToInt32(gameId));
                    if (timer != null)
                        timer.Restart();
                }

                return Json(new { success = true });
            }
            else 
            {
                return Json(new { error = result.Errors[0] });
            }
        }

        public async Task<JsonResult> LeaveGame(int gameId, string quitUsername, string opponent1Username, string opponent2Username)
        {
            var result = await _gameService.LeaveGame(gameId, quitUsername);
            if (result.IsSucessfull)
            {
                await _gameHubContext.Clients.Group("Game" + gameId).SendAsync("GameQuit", quitUsername, opponent1Username, opponent2Username);
                await _lobbyHubContext.Clients.All.SendAsync("UpdateRoomList");
                TimerHelper.RemoveTimerForGameId(gameId);
                
                return Json(new { success = true });
            }
            else 
            {
                return Json(new { error = result.Errors[0] });
            }
        }
    }
}
