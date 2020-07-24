using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Entities
{
    public class GameAction : BaseEntity
    {
        public int RoundId { get; set; }
        public Round Round { get; set; }
        public int TurnNumber { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
        public Call? Call { get; set; }
        public string HighestCardInACall { get; set; }
        public CardSuit? ChosenTrump { get; set; }
        public string CardPlayed { get; set; }
    }
}
