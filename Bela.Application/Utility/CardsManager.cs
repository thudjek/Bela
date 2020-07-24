using Bela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Bela.Domain.Enums;
using System.Linq;

namespace Bela.Application.Utility
{
    public class CardsManager
    {
        private static List<Card> FullDeckOfCards = new List<Card>()
        {
            new Card(CardSuit.Hearts, CardValue.Ace, ""),
            new Card(CardSuit.Hearts, CardValue.King, ""),
            new Card(CardSuit.Hearts, CardValue.Queen, ""),
            new Card(CardSuit.Hearts, CardValue.Jack, ""),
            new Card(CardSuit.Hearts, CardValue.Ten, ""),
            new Card(CardSuit.Hearts, CardValue.Nine, ""),
            new Card(CardSuit.Hearts, CardValue.Eight, ""),
            new Card(CardSuit.Hearts, CardValue.Seven, ""),
            new Card(CardSuit.Spades, CardValue.Ace, ""),
            new Card(CardSuit.Spades, CardValue.King, ""),
            new Card(CardSuit.Spades, CardValue.Queen, ""),
            new Card(CardSuit.Spades, CardValue.Jack, ""),
            new Card(CardSuit.Spades, CardValue.Ten, ""),
            new Card(CardSuit.Spades, CardValue.Nine, ""),
            new Card(CardSuit.Spades, CardValue.Eight, ""),
            new Card(CardSuit.Spades, CardValue.Seven, ""),
            new Card(CardSuit.Diamonds, CardValue.Ace, ""),
            new Card(CardSuit.Diamonds, CardValue.King, ""),
            new Card(CardSuit.Diamonds, CardValue.Queen, ""),
            new Card(CardSuit.Diamonds, CardValue.Jack, ""),
            new Card(CardSuit.Diamonds, CardValue.Ten, ""),
            new Card(CardSuit.Diamonds, CardValue.Nine, ""),
            new Card(CardSuit.Diamonds, CardValue.Eight, ""),
            new Card(CardSuit.Diamonds, CardValue.Seven, ""),
            new Card(CardSuit.Clubs, CardValue.Ace, ""),
            new Card(CardSuit.Clubs, CardValue.King, ""),
            new Card(CardSuit.Clubs, CardValue.Queen, ""),
            new Card(CardSuit.Clubs, CardValue.Jack, ""),
            new Card(CardSuit.Clubs, CardValue.Ten, ""),
            new Card(CardSuit.Clubs, CardValue.Nine, ""),
            new Card(CardSuit.Clubs, CardValue.Eight, ""),
            new Card(CardSuit.Clubs, CardValue.Seven, "")
        };

        public static Card GetCardFromString(string cardString)
        {
            char suitChar = cardString[cardString.Length - 1];
            char valueChar = cardString[0];
            CardSuit suit = GetCardSuitFromChar(suitChar);
            CardValue value = GetCardValueFromChar(valueChar);
            string imgPath = FullDeckOfCards.Where(c => c.Suit == suit && c.Value == value).FirstOrDefault().ImgPath;
            return new Card(suit, value, imgPath);
        }

        public static CardSuit GetCardSuitFromChar(char suitChar)
        {
            switch (suitChar)
            {
                case 'H':
                    return CardSuit.Hearts;
                case 'S':
                    return CardSuit.Spades;
                case 'D':
                    return CardSuit.Diamonds;
                case 'C':
                    return CardSuit.Clubs;
                default:
                    throw new ArgumentException();
            }
        }

        public static CardValue GetCardValueFromChar(char valueChar)
        {
            switch (valueChar)
            {
                case 'A':
                    return CardValue.Ace;
                case 'K':
                    return CardValue.King;
                case 'Q':
                    return CardValue.Queen;
                case 'J':
                    return CardValue.Jack;
                case '1':
                    return CardValue.Ten;
                case '9':
                    return CardValue.Nine;
                case '8':
                    return CardValue.Eight;
                case '7':
                    return CardValue.Seven;
                default:
                    throw new ArgumentException();
            }
        }

        public static List<Card> GetShuffledDeck()
        {
            
            var deck = FullDeckOfCards;
            var n = deck.Count;

            while (n > 1)
            {
                var rand = RandomGenerator.Next(0, n--);
                var temp = deck[n];
                deck[n] = deck[rand];
                deck[rand] = temp;
            }

            return deck;
        }
    }
}
