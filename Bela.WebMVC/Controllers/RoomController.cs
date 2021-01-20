using Bela.Application.Interfaces;
using Bela.Application.Utility;
using Bela.Application.ViewModels.Room;
using Bela.Domain.Entities;
using Bela.WebMVC.Extensions;
using Bela.WebMVC.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Bela.WebMVC.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IGameService _gameService;
        private readonly IIdentityService _identityService;
        private readonly IHubContext<LobbyHub> _lobbyHubContext;
        private readonly IHubContext<RoomHub> _roomHubContext;
        private readonly IHubContext<GameHub> _gameHubContext;
        private readonly IHubContext<MainHub> _mainHubContext;

        public RoomController(
            IRoomService roomService,
            IGameService gameService,
            IIdentityService identityService,
            IHubContext<LobbyHub> lobbyHubContext,
            IHubContext<RoomHub> roomHubContext,
            IHubContext<GameHub> gameHubContext,
            IHubContext<MainHub> mainHubContext)
        {
            _roomService = roomService;
            _gameService = gameService;
            _identityService = identityService;
            _lobbyHubContext = lobbyHubContext;
            _roomHubContext = roomHubContext;
            _gameHubContext = gameHubContext;
            _mainHubContext = mainHubContext;
        }

        public async Task<IActionResult> Index()
        {
            var roomId = await _identityService.GetUsersRoomId(User.GetUserId());
            if (roomId == 0)
                return RedirectToAction("Index", "Lobby");

            var userId = User.GetUserId();
            if (await _gameService.IsPlayerInGame(userId))
                return RedirectToAction("Index", "Game");

            var model = await _roomService.GetRoomViewModelAsync(roomId, User.GetUserId(), User.GetUserName());
            return View(model);
        }

        public async Task<JsonResult> CreateRoom(CreateRoomModel model)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (IsCreateRoomModelValid(model, errors))
            {
                var userId = User.GetUserId();
                var roomId = await _roomService.CreateRoomAsync(model, userId);
                if (roomId > 0)
                {
                    await _lobbyHubContext.Clients.All.SendAsync("UpdateRoomList");
                    await _roomHubContext.Clients.All.SendAsync("UpdateUserList");
                    await _lobbyHubContext.Clients.All.SendAsync("UpdateUserList");
                    return Json(new { success = true });
                }  
            }
            return Json(new { errors });
        }

        public async Task<JsonResult> LeaveRoom(int roomId)
        {
            var result = await _roomService.LeaveRoom(roomId, User.GetUserId());

            if (result)
            {
                await _roomHubContext.Clients.Group("Room" + roomId.ToString()).SendAsync("UpdateUsersLayout");
                await _roomHubContext.Clients.All.SendAsync("UpdateUserList");
                await _lobbyHubContext.Clients.All.SendAsync("UpdateUserList");
                await _lobbyHubContext.Clients.All.SendAsync("UpdateRoomList");
                return Json(new { success = true });
            }
            else 
            {
                return Json(new { success = false });
            }
        }

        public async Task<IActionResult> DropRoom(int roomId)
        {
            var result = await _roomService.DropRoom(roomId, User.GetUserId());
            if (result.IsSucessfull)
            {
                var connIdString = result.Values[0] as string;
                string[] connIdArray = connIdString.Split(",");
                await _mainHubContext.Clients.Clients(connIdArray).SendAsync("RoomDroped");
            }
            await _roomHubContext.Clients.All.SendAsync("UpdateUserList");
            await _lobbyHubContext.Clients.All.SendAsync("UpdateUserList");
            await _lobbyHubContext.Clients.All.SendAsync("UpdateRoomList");

            return RedirectToAction("Index", "Lobby");
        }

        public async Task<JsonResult> JoinRoom(int roomId, bool isPrivate, string roomPassword = "", bool isInvite = false)
        {
            if (isPrivate && string.IsNullOrEmpty(roomPassword))
                return Json(new { error = "Unesite lozinku sobe" });

            int userId = User.GetUserId();
            var result = await _roomService.TryJoinRoomAsync(roomId, userId, isPrivate, roomPassword, isInvite);
            if (result.IsSucessfull)
            {
                await _roomHubContext.Clients.Group("Room" + roomId.ToString()).SendAsync("UpdateUsersLayout");
                await _roomHubContext.Clients.All.SendAsync("UpdateUserList");
                await _lobbyHubContext.Clients.All.SendAsync("UpdateUserList");
                await _lobbyHubContext.Clients.All.SendAsync("UpdateRoomList");
                return Json(new { success = true });
            }
            else
                return Json(new { error = result.Errors[0] });
        }

        public async Task<JsonResult> KickFromRoom(int userId, int roomId)
        {
            var result = await _roomService.KickUserFromRoom(userId, roomId);
            if (result.IsSucessfull)
            {
                var connId = result.Values[0] as string;
                if (connId != null)
                {
                    await _mainHubContext.Clients.Client(connId).SendAsync("KickedFromRoom");
                    await _roomHubContext.Clients.All.SendAsync("UpdateUserList");
                    await _lobbyHubContext.Clients.All.SendAsync("UpdateUserList");
                }
                    
                await _roomHubContext.Clients.Group("Room" + roomId.ToString()).SendAsync("UpdateUsersLayout");
                
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public async Task<JsonResult> ToggleReady(int roomId)
        {
            int userId = User.GetUserId();
            var result = await _roomService.ToggleReady(roomId, userId);
            if (result)
            {
                await _roomHubContext.Clients.Group("Room" + roomId.ToString()).SendAsync("UpdateUsersLayout");
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public async Task<JsonResult> SwapPlayers(int firstUserId, int secondUserId, int roomId)
        {
            var result = await _roomService.SwapPlayers(firstUserId, secondUserId, roomId);
            if (result)
            {
                await _roomHubContext.Clients.Group("Room" + roomId.ToString()).SendAsync("UpdateUsersLayout");
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public async Task<JsonResult> TryStartGame(int roomId)
        {
            var result = await _roomService.CanGameBeStarted(roomId);
            if (result.IsSucessfull)
            {
                result = await _gameService.IsRoomInGame(roomId);
                if (!result.IsSucessfull)
                {
                    result = await _gameService.StartGame(roomId);
                    if (result.IsSucessfull)
                    {
                        await _roomHubContext.Clients.Group("Room" + roomId.ToString()).SendAsync("GameStarted");
                        await _lobbyHubContext.Clients.All.SendAsync("UpdateRoomList");
                        var gameId = (int)result.Values[0];
                        TimerBela timer = new TimerBela(18000, OnTimerElapsed, gameId);
                        TimerHelper.AddTimerForGameId(gameId, timer);
                        timer.Start();
                        return Json(new { success = true });
                    }
                    else 
                    {
                        return Json(new { error = result.Errors[0] });
                    }
                }
                else
                {
                    return Json(new { error = result.Errors[0] });
                }
            }
            else
            {
                return Json(new { error = result.Errors[0] });
            }
        }

        private bool IsCreateRoomModelValid(CreateRoomModel model, Dictionary<string, string> errors)
        {
            if (string.IsNullOrEmpty(model.RoomName))
                errors.Add("roomName", "Unesite naziv sobe");
            else if(model.RoomName.Length > 26)
                errors.Add("roomName", "Unesite kraći naziv sobe");

            if (model.IsPrivate && string.IsNullOrEmpty(model.RoomPassword))
                errors.Add("roomPassword", "Unesite lozinku sobe");

            return errors.Count == 0;
        }

        public IActionResult GetRoomUsersLayoutViewComponent(int roomId, bool isOwner)
        {
            return ViewComponent("RoomUsersLayout", new { roomId, isOwner });
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            TimerBela timer = sender as TimerBela;
            var gameId = timer.GameId;
            var result = _gameService.LeaveGameTimerElapsed(gameId);
            if (result.IsSucessfull)
            {
                var quitUsername = result.Values[0] as string;
                var opponent1Username = result.Values[1] as string;
                var opponent2Username = result.Values[2] as string;
                _gameHubContext.Clients.Group("Game" + gameId.ToString()).SendAsync("TimerElapsed", quitUsername, opponent1Username, opponent2Username);
                _lobbyHubContext.Clients.All.SendAsync("UpdateRoomList");
            }

        }
    }
}
