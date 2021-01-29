using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bela.WebMVC.Extensions;
using System.Security.Claims;
using Bela.Application.Interfaces;
using System.Threading;
using Bela.Application.Utility;
using Microsoft.Extensions.Logging;

namespace Bela.WebMVC.Hubs
{
    public class MainHub : Hub
    {
        private const int sec = 1000;

        private readonly IIdentityService _identityService;
        private readonly IHubContext<LobbyHub> _lobbyHubContext;
        private readonly IHubContext<RoomHub> _roomHubContext;

        public MainHub(
            IIdentityService identityService,
            IHubContext<LobbyHub> lobbyHubContext,
            IHubContext<RoomHub> roomHubContext)
        {
            _identityService = identityService;
            _lobbyHubContext = lobbyHubContext;
            _roomHubContext = roomHubContext;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId();
            await _identityService.SetUsersMainHubConnectionId(userId, Context.ConnectionId);
            await base.OnConnectedAsync(); 
        }

        public async Task SendInviteToUser(int userId, string ownerUsername, int roomId)
        {
            var connectionId = await _identityService.GetUsersMainHubConnectionId(userId);
            await Clients.Client(connectionId).SendAsync("SendInviteToUser", ownerUsername, roomId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = GetUserId();
            var result = await _identityService.DeleteUsersMainHubConnectionId(userId);
            if (result.IsSucessfull && result.Values != null)
                await _roomHubContext.Clients.Group("Room" + result.Values[0].ToString()).SendAsync("UpdateUsersLayout");

            Thread.Sleep(20 * sec);

            var connectionId = await _identityService.GetUsersMainHubConnectionId(userId);

            if (connectionId != null)
            {
                await Task.FromResult(0);
            }
            else
            {
                await OnDisconnectUserUpdate(userId);
                await base.OnDisconnectedAsync(exception);
            }

            await base.OnDisconnectedAsync(exception);
        }

        private int GetUserId()
        {
            var httpContext = Context.GetHttpContext();
            return httpContext.User.GetUserId();
        }

        private async Task OnDisconnectUserUpdate(int userId)
        {
            await _identityService.LogOutUser(userId, false);
            await _roomHubContext.Clients.All.SendAsync("UpdateUserList");
            await _lobbyHubContext.Clients.All.SendAsync("UpdateUserList");
        }
    }
}
