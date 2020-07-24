using Bela.Domain.Enums;
using Bela.Domain.Extensions;

namespace Bela.Domain.Entities
{
    public class Card
    {
        public Card(CardSuit suit, CardValue value, string imgPath = "")
        {
            Suit = suit;
            Value = value;
            ImgPath = imgPath;
        }

        public CardSuit Suit { get; private set; }
        public CardValue Value { get; private set; }
        public string ImgPath { get; private set; }

        public override string ToString()
        {
            return Value.GetStringValue() + Suit.GetStringValue();
        }
    }
}
