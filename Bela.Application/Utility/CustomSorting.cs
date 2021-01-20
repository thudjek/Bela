using Bela.Domain.Enums;
using Bela.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.Utility
{
    public class CustomSorting
    {
        public static CardValue[] FourOfAKindSortOrder = new[]
        {
            CardValue.Ace,
            CardValue.Ten,
            CardValue.King,
            CardValue.Queen,
            CardValue.Jack,
            CardValue.Nine,
            CardValue.Eight,
            CardValue.Seven
        };

        public static PlayerPosition[] GetPositionSortingOrderForCalls(PlayerPosition firstToPlayInRound)
        {
            return new[] 
            {
                firstToPlayInRound,
                firstToPlayInRound.GetNextPosition(),
                firstToPlayInRound.GetTeammatePosition(),
                firstToPlayInRound.GetPreviousPosition()
            };
        }
    }
}
