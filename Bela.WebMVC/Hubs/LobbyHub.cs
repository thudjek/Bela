using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bela.WebMVC.Hubs
{
    public class LobbyHub : Hub
    {
        public LobbyHub()
        {

        }

        public override Task OnConnectedAsync()
        {
            return Clients.Caller.SendAsync("TryJoinAuthGroup");
        }

        public Task JoinAuthGroup()
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, "AuthGroup");
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
