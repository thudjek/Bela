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
        public RoundPhase RoundPhase { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
        public Call? Call { get; set; }
        public CardValue? HighestValueInACall { get; set; }
        public string CardPlayed { get; set; }
    }
}
