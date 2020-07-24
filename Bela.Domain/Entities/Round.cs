using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Entities
{
    public class Round : BaseEntity
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int RoundNumber { get; set; }
        public int TurnNumber { get; set; }
        public RoundPhase RoundPhase { get; set; }
        public int FirstTeamScore { get; set; }
        public int SecondTeamScore { get; set; }
        public int FirstTeamCalls { get; set; }
        public int SecondTeamCalls { get; set; }
        public PlayerPosition FirstPlayerToPlay { get; set; }
        public PlayerPosition CurrentPlayerToPlay { get; set; }
        public CardSuit? CurrentTrump { get; set; }
        public List<GameAction> GameActions { get; set; }

    }
}
