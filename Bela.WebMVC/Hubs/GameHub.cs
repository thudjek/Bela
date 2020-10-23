using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bela.WebMVC.Hubs
{
    public class GameHub : Hub
    {
        public GameHub()
        {

        }

        public override Task OnConnectedAsync()
        {
            return Clients.Caller.SendAsync("JoinGameGroup");
        }

        public Task JoinGameGroup(int gameId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, "Game" + gameId.ToString());
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
