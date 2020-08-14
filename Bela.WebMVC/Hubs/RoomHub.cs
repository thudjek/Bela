using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bela.WebMVC.Hubs
{
    public class RoomHub : Hub
    {
        public RoomHub()
        {

        }

        public override Task OnConnectedAsync()
        {
            return Clients.Caller.SendAsync("JoinRoomGroup");
        }

        public Task JoinRoomGroup(int roomId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, "Room" + roomId.ToString());
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
