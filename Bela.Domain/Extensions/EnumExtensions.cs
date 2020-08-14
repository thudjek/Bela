using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Bela.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            DisplayAttribute displayAttribute = (DisplayAttribute)enumValue.GetType()
                                                .GetMember(enumValue.ToString())
                                                .FirstOrDefault()?
                                                .GetCustomAttributes(typeof(DisplayAttribute), false)
                                                .FirstOrDefault();

            return displayAttribute != null ? displayAttribute.Name : "";
        }
    }
}
