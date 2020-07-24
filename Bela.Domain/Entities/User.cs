using System;
using System.Collections.Generic;
using System.Text;
using Bela.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Bela.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public DateTime RegistrationDate { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Dropouts { get; set; }
        public UserStatus UserStatus { get; set; }
        public Player Player { get; set; }
    }
}
