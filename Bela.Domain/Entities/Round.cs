using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Entities
{
    public class Round : BaseEntity
    {
        public Round()
        {
            CardsPlayed = new List<CardPlayed>();
            Calls = new List<Call>();
        }

        public int GameId { get; set; }
        public Game Game { get; set; }
        public int RoundNumber { get; set; }
        public RoundPhase RoundPhase { get; set; }
        public int FirstTeamCalls { get; set; }
        public int SecondTeamCalls { get; set; }
        public int FirstTeamScore { get; set; }
        public int SecondTeamScore { get; set; }
        public int FirstTeamRoundTotal { get; set; }
        public int SecondTeamRoundTotal { get; set; }
        public PlayerPosition FirstPlayerToPlay { get; set; }
        public PlayerPosition CurrentPlayerToPlay { get; set; }
        public CardSuit? CurrentTrump { get; set; }
        public string TrumpSelectedBy { get; set; }
        public List<CardPlayed> CardsPlayed { get; set; }
        public List<Call> Calls { get; set; }

    }
}
