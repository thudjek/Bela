using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Extensions
{
    public static class CallTypeExtensions
    {
        public static int GetValue(this CallType type)
        {
            return type switch
            {
                CallType.ThreeInARow => 20,
                CallType.FourInARow => 50,
                CallType.FiveInARow => 100,
                CallType.SixInARow => 100,
                CallType.SevenInARow => 100,
                CallType.EightInARow => 100,
                CallType.FourOfAKind => 100,
                CallType.FourNines => 150,
                CallType.FourJacks => 200,
                _ => 0
            };
        }
    }
}
