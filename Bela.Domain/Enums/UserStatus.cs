using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bela.Domain.Enums
{
    public enum UserStatus
    {
        [Display(Name = "Offline")]
        Offline = 0,

        [Display(Name = "Online")]
        Online = 1,

        [Display(Name = "U sobi")]
        InRoom = 2,

        [Display(Name = "U igri")]
        InGame = 3
    }
}
