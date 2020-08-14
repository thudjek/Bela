using Bela.Application.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.ViewModels.Room
{
    public class CreateRoomModel
    {
        public string RoomName { get; set; }
        public bool IsPrivate { get; set; }
        public string RoomPassword { get; set; }
    }
}
