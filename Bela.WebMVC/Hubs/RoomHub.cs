using Bela.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bela.WebMVC.Extensions;
using System.Threading;

namespace Bela.WebMVC.Hubs
{
    public class RoomHub : Hub
    {
        private readonly IIdentityService _identityService;
        private readonly IHubContext<RoomHub> _roomHubContext;
        public RoomHub(
            IIdentityService identityService,
            IHubContext<RoomHub> roomHubContext)
        {
            _identityService = identityService;
            _roomHubContext = roomHubContext;
        }

        public override Task OnConnectedAsync()
        {
            return Clients.Caller.SendAsync("JoinRoomGroup");
        }

        public Task JoinRoomGroup(int roomId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, "Room" + roomId.ToString());
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        private int GetUserId()
        {
            var httpContext = Context.GetHttpContext();
            return httpContext.User.GetUserId();
        }
    }
}
