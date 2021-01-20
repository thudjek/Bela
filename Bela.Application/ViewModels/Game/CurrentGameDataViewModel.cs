using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.ViewModels.Game
{
    public class CurrentGameDataViewModel
    {
        public CurrentGameDataViewModel()
        {
            Rounds = new List<RoundShort>();
        }

        public int CurrentRoundId { get; set; }
        public int CurrentRoundPhase { get; set; }
        public bool isLast { get; set; }
        public int MiCalls { get; set; }
        public int ViCalls { get; set; }
        public int MiPoints { get; set; }
        public int ViPoints { get; set; }
        public int MiRoundTotal { get; set; }
        public int ViRoundTotal { get; set; }
        public int MiTotalScore { get; set; }
        public int ViTotalScore { get; set; }
        public int SelectedTrump { get; set; }
        public string TrumpSelectedBy { get; set; }
        public int PositionToPlay { get; set; }
        public int DealerPosition { get; set; }
        public string LeftCard { get; set; }
        public string RightCard { get; set; }
        public string UpCard { get; set; }
        public string DownCard { get; set; }
        public int LeftCallValue { get; set; }
        public int RightCallValue { get; set; }
        public int UpCallValue { get; set; }
        public int DownCallValue { get; set; }
        public double RemainingTime { get; set; }
        public List<RoundShort> Rounds { get; set; }
    }

    public class RoundShort
    {
        public int Number { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
    }
}
