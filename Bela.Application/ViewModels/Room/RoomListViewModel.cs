using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.ViewModels.Room
{
    public class RoomListViewModel
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public bool IsPrivate { get; set; }
        public int PlayerCount { get; set; }
    }
}
