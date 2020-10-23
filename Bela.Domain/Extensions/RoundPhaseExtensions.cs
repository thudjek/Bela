using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Extensions
{
    public static class RoundPhaseExtensions
    {
        public static RoundPhase GetNextPhase(this RoundPhase phase)
        {
            var currentPhaseInt = (int)phase;
            currentPhaseInt++;
            return (RoundPhase)currentPhaseInt;
        }
    }
}
