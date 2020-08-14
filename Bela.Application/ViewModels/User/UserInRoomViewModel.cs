using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.ViewModels.User
{
    public class UserInRoomViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Dropouts { get; set; }
        public bool IsReady { get; set; }
        public int? RoomOrderNumber { get; set; }
    }
}
