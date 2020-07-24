using Bela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bela.Domain.Extensions
{
    public static class ListExtensions
    {
        public static string DealCards(this List<Card> source, int numOfCards)
        {
            List<Card> result = new List<Card>();
            if (numOfCards > source.Count)
                throw new ArgumentException();

            if (numOfCards > 0)
            {
                for (int i = 0; i < numOfCards; i++)
                {
                    result.Add(source[0]);
                    source.RemoveAt(0);
                }
                result = result.OrderCards();
                return string.Join(",", result);
            }
            else {
                throw new ArgumentException();
            }
        }

        public static List<Card> OrderCards(this List<Card> hand)
        {
            return hand.OrderBy(c => c.Suit).ThenBy(c => c.Value).ToList();
        }


    }
}
