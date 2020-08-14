using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bela.Domain.Enums
{
    public enum Gender
    {
        [Display(Name = "Muško")]
        Male = 1,

        [Display(Name = "Žensko")]
        Female = 2
    }
}
