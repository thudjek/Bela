using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.ViewModels.Game
{
    public class TotalScoresAndRoundsModel
    {
        public TotalScoresAndRoundsModel()
        {
            Scores = new Dictionary<string, int>();
            Rounds = new List<RoundShort>();
        }
        public Dictionary<string, int> Scores { get; set; }
        public List<RoundShort> Rounds { get; set; }
    }
}
