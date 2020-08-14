using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bela.WebMVC.Extensions;
using System.Security.Claims;
using Bela.Application.Interfaces;

namespace Bela.WebMVC.Hubs
{
    public class MainHub : Hub
    {
        public readonly IIdentityService _identityService;
        public MainHub(IIdentityService identityService)
        {
            _identityService = identityService;
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
            await _identityService.DeleteUsersMainHubConnectionId(userId);
            await base.OnDisconnectedAsync(exception);
        }

        private int GetUserId()
        {
            var httpContext = Context.GetHttpContext();
            return httpContext.User.GetUserId();
        }
    }
}
