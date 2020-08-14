using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bela.WebMVC.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddErrors(this ModelStateDictionary modelState, IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                modelState.TryAddModelError("", error);
            }
        }
    }
}
