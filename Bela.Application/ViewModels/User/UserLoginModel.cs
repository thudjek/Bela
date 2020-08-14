using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bela.Application.ViewModels.User
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Korisnično ime je obavezno polje")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezno polje")]
        public string Password { get; set; }
    }
}
