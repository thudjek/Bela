using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Extensions
{
    public static class CardSuitExtensions
    {
        public static string GetStringValue(this CardSuit suit)
        {
            return Enum.GetName(typeof(CardSuit), suit)[0].ToString();
        }
    }
}
