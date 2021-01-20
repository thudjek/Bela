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
            new Card(CardSuit.Hearts, CardValue.Ace, "imgs/cards/herc_as.png"),
            new Card(CardSuit.Hearts, CardValue.King, "imgs/cards/herc_kralj.png"),
            new Card(CardSuit.Hearts, CardValue.Queen, "imgs/cards/herc_baba.png"),
            new Card(CardSuit.Hearts, CardValue.Jack, "imgs/cards/herc_decko.png"),
            new Card(CardSuit.Hearts, CardValue.Ten, "imgs/cards/herc_deset.png"),
            new Card(CardSuit.Hearts, CardValue.Nine, "imgs/cards/herc_devet.png"),
            new Card(CardSuit.Hearts, CardValue.Eight, "imgs/cards/herc_osam.png"),
            new Card(CardSuit.Hearts, CardValue.Seven, "imgs/cards/herc_sedam.png"),
            new Card(CardSuit.Spades, CardValue.Ace, "imgs/cards/pik_as.png"),
            new Card(CardSuit.Spades, CardValue.King, "imgs/cards/pik_kralj.png"),
            new Card(CardSuit.Spades, CardValue.Queen, "imgs/cards/pik_baba.png"),
            new Card(CardSuit.Spades, CardValue.Jack, "imgs/cards/pik_decko.png"),
            new Card(CardSuit.Spades, CardValue.Ten, "imgs/cards/pik_deset.png"),
            new Card(CardSuit.Spades, CardValue.Nine, "imgs/cards/pik_devet.png"),
            new Card(CardSuit.Spades, CardValue.Eight, "imgs/cards/pik_osam.png"),
            new Card(CardSuit.Spades, CardValue.Seven, "imgs/cards/pik_sedam.png"),
            new Card(CardSuit.Diamonds, CardValue.Ace, "imgs/cards/karo_as.png"),
            new Card(CardSuit.Diamonds, CardValue.King, "imgs/cards/karo_kralj.png"),
            new Card(CardSuit.Diamonds, CardValue.Queen, "imgs/cards/karo_baba.png"),
            new Card(CardSuit.Diamonds, CardValue.Jack, "imgs/cards/karo_decko.png"),
            new Card(CardSuit.Diamonds, CardValue.Ten, "imgs/cards/karo_deset.png"),
            new Card(CardSuit.Diamonds, CardValue.Nine, "imgs/cards/karo_devet.png"),
            new Card(CardSuit.Diamonds, CardValue.Eight, "imgs/cards/karo_osam.png"),
            new Card(CardSuit.Diamonds, CardValue.Seven, "imgs/cards/karo_sedam.png"),
            new Card(CardSuit.Clubs, CardValue.Ace, "imgs/cards/tref_as.png"),
            new Card(CardSuit.Clubs, CardValue.King, "imgs/cards/tref_kralj.png"),
            new Card(CardSuit.Clubs, CardValue.Queen, "imgs/cards/tref_baba.png"),
            new Card(CardSuit.Clubs, CardValue.Jack, "imgs/cards/tref_decko.png"),
            new Card(CardSuit.Clubs, CardValue.Ten, "imgs/cards/tref_deset.png"),
            new Card(CardSuit.Clubs, CardValue.Nine, "imgs/cards/tref_devet.png"),
            new Card(CardSuit.Clubs, CardValue.Eight, "imgs/cards/tref_osam.png"),
            new Card(CardSuit.Clubs, CardValue.Seven, "imgs/cards/tref_sedam.png")
        };

        public static Card GetCard(CardSuit suit, CardValue value)
        {
            return FullDeckOfCards.FirstOrDefault(c => c.Suit == suit && c.Value == value);
        }

        public static List<Card> GetFourOfAKind(CardValue value)
        {
            return FullDeckOfCards.Where(c => c.Value == value).OrderBy(c => c.Suit).ToList();
        }

        public static List<Card> GetSequence(CardSuit suit, CardValue value, CallType type)
        {
            if (type == CallType.EightInARow)
                return FullDeckOfCards.Where(c => c.Suit == suit).OrderBy(c => c.Value).ToList();

            List<Card> cards = new List<Card>();
            int counter = ((int)type) + 2;
            int valueInt = (int)value;
            for (int i = 0; i < counter; i++)
            {
                cards.Add(GetCard(suit, (CardValue)valueInt));
                valueInt--;
            }

            return cards.OrderBy(c => c.Value).ToList();
        }

        public static string GetBackgroundCardImgPath()
        {
            return "imgs/cards/card_bgd.png";
        }

        public static Card GetCardFromString(string cardString)
        {
            char suitChar = cardString[cardString.Length - 1];
            char valueChar = cardString[0];
            CardSuit suit = GetCardSuitFromChar(suitChar);
            CardValue value = GetCardValueFromChar(valueChar);
            return GetCard(suit, value);
        }

        public static List<Card> GetCardListFromHandString(string handString)
        {
            List<Card> cards = new List<Card>();
            var cardStrings = handString.Split(",").ToList();
            foreach (var cardString in cardStrings)
            {
                cards.Add(GetCardFromString(cardString));
            }
            return cards;
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
            var deck = new List<Card>(FullDeckOfCards);
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
