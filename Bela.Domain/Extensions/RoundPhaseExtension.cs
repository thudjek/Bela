using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Extensions
{
    public static class RoundPhaseExtension
    {
        public static RoundPhase GetNextRoundPhase(this RoundPhase phase)
        {
            int phaseInt = (int)phase;
            phaseInt++;
            return (RoundPhase)phaseInt;
        }
    }
}
