using Bela.Domain.Enums;
using Bela.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bela.Domain.Entities
{
    public class Call : BaseEntity
    {
        public Call()
        {
        }

        public Call(PlayerPosition position, CallType type, string card)
        {
            PlayerPosition = position;
            Type = type;
            Value = type.GetValue();
            HighestCard = card;
        }

        public int RoundId { get; set; }
        public Round Round { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
        public CallType Type { get; set; }
        public int Value { get; set; }
        public string HighestCard { get; set; }
    }
}
