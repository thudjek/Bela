using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.ViewModels.Room
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public bool IsPrivate { get; set; }
        public string RoomPassword { get; set; }
        public string OwnerUsername { get; set; }
        public bool IsOwner { get; set; }
    }
}
