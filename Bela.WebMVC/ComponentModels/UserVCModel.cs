using Bela.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bela.WebMVC.ComponentModels
{
    public class UserVCModel
    {
        public List<UserListViewModel> Users { get; set; }
        public bool isRoomAndOwner { get; set; }
    }
}
