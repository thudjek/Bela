using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Entities
{
    public class CardPlayed : BaseEntity
    {
        public CardPlayed()
        {
        }

        public CardPlayed(RoundPhase roundPhase, PlayerPosition playerPosition, Card card, CardSuit trump)
        {
            RoundPhase = roundPhase;
            PlayerPosition = playerPosition;
            CardString = card.ToString();
            Value = card.GetPointsValue(trump);
        }

        public int RoundId { get; set; }
        public Round Round { get; set; }
        public RoundPhase RoundPhase { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
        public string CardString { get; set; }
        public int Value { get; set; }
    }
}
