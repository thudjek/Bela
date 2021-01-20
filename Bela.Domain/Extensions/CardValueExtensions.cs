using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Extensions
{
    public static class CardValueExtensions
    {
        public static string GetStringValue(this CardValue cardValue)
        {
            if (cardValue == CardValue.Ace || cardValue == CardValue.King || cardValue == CardValue.Queen || cardValue == CardValue.Jack)
            {
                return Enum.GetName(typeof(CardValue), cardValue)[0].ToString();
            }
            else
            {
                return cardValue switch
                {
                    CardValue.Seven => "7",
                    CardValue.Eight => "8",
                    CardValue.Nine => "9",
                    CardValue.Ten => "10",
                    _ => ""
                };
            }
        }
    }
}
