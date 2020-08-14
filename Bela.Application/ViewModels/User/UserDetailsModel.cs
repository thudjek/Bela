using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.ViewModels.User
{
    public class UserDetailsModel
    {
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string RegistrationDate { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Dropouts { get; set; }
        
    }
}
