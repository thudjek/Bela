using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Application.ViewModels.Game
{
    public class EndScreenViewModel
    {
        public string Winner1 { get; set; }
        public string Winner2 { get; set; }
        public string MyUsername { get; set; }
        public string TeammateUsername { get; set; }
        public string Opponent1Username { get; set; }
        public string Opponent2Username { get; set; }
        public int MyTeamScore { get; set; }
        public int OpponentTeamScore { get; set; }
        public bool PlayerQuit { get; set; }
        public string QuitUsername { get; set; }
    }
}
