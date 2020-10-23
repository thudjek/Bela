using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.ViewModels.Game
{
    public class CurrentGameDataViewModel
    {
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
    }
}
