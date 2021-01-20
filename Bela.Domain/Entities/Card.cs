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

        public int GetPointsValue(CardSuit trump)
        {
            switch (Value)
            {
                case CardValue.Seven:
                    return 0;
                case CardValue.Eight:
                    return 0;
                case CardValue.Nine:
                    return Suit == trump ? 14 : 0;
                case CardValue.Ten:
                    return 10;
                case CardValue.Jack:
                    return Suit == trump ? 20 : 2;
                case CardValue.Queen:
                    return 3;
                case CardValue.King:
                    return 4;
                case CardValue.Ace:
                    return 11;
                default:
                    return 0;
            }
        }

        public int GetValueOrder(bool isTrump)
        {
            switch (Value)
            {
                case CardValue.Seven:
                    return 1;
                case CardValue.Eight:
                    return 2;
                case CardValue.Nine:
                    return isTrump ? 7 : 3;
                case CardValue.Ten:
                    return isTrump ? 5 : 7;
                case CardValue.Jack:
                    return isTrump ? 8 : 4;
                case CardValue.Queen:
                    return isTrump ? 3 : 5;
                case CardValue.King:
                    return isTrump ? 4 : 6;
                case CardValue.Ace:
                    return isTrump ? 6 : 8;
                default:
                    return 0;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Card))
                return false;

            Card card = obj as Card;
            return Suit == card.Suit && Value == card.Value;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Suit.GetHashCode();
                hash = hash * 23 + Value.GetHashCode();
                return hash;
            }
        }
    }
}
