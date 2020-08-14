using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bela.Application.ViewModels.User
{
    public class UserRegisterModel
    {
        [StringLength(15, ErrorMessage = "Korisničko ime može sadržavati najviše 15 znakova")]
        [Required(ErrorMessage = "Korisničko ime je obavezno polje")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Email nije valjan")]
        [Required(ErrorMessage = "Email je obavezno polje")]
        public string Email { get; set; }
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezno polje")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Potrebno potvrditi lozinku")]
        public string ConfirmPassword { get; set; }

    }
}
