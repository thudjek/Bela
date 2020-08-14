using Bela.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bela.WebMVC.ComponentModels
{
    public class RoomUsersLayoutVCModel
    {
        public List<UserInRoomViewModel> Users { get; set; }
        public bool IsOwner { get; set; }
    }
}
