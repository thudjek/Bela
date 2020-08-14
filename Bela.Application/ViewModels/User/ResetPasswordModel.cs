using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bela.Application.ViewModels.User
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Lozinka je obavezno polje")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Potrebno potvrditi lozinku")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
