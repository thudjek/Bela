using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bela.Application.ViewModels.User
{
    public class ForgottenPasswordModel
    {
        [EmailAddress(ErrorMessage = "Email nije valjan")]
        [Required(ErrorMessage = "Email je obavezno polje")]
        public string Email { get; set; }
    }
}
