using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.ViewModels.User
{
    public class UserListViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
