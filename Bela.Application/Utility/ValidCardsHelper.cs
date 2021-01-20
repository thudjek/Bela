using Bela.Domain.Entities;
using Bela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bela.Application.Utility
{
    public class ValidCardsHelper
    {
        public static List<Card> GetValidCardsForPlay(List<Card> cardsInHand, CardSuit trump, List<Card> cardsPlayed)
        {
            if (cardsPlayed.Count == 0 || cardsInHand.Count == 1)
                return cardsInHand;

            var firstCardSuit = cardsPlayed[0].Suit;

            if (firstCardSuit == trump)
            {
                return GetValidCardsForPlayTrumpFirst(cardsInHand, cardsPlayed, firstCardSuit);
            }

            return GetValidCardsForPlayNonTrumpFirst(cardsInHand, cardsPlayed, trump, firstCardSuit);
        }

        private static List<Card> GetValidCardsForPlayTrumpFirst(List<Card> cardsInHand, List<Card> cardsPlayed, CardSuit firstCardSuit)
        {
            if (cardsInHand.Any(c => c.Suit == firstCardSuit))
            {
                var strongestCard = GetStrongestTrumpCard(cardsPlayed.Where(c => c.Suit == firstCardSuit).ToList(), firstCardSuit);
                if (cardsInHand.Any(c => c.Suit == firstCardSuit && c.GetValueOrder(true) > strongestCard.GetValueOrder(true)))
                {
                    return cardsInHand.Where(c => c.Suit == firstCardSuit && c.GetValueOrder(true) > strongestCard.GetValueOrder(true)).ToList();
                }

                return cardsInHand.Where(c => c.Suit == firstCardSuit).ToList();
            }

            return cardsInHand;
        }

        private static List<Card> GetValidCardsForPlayNonTrumpFirst(List<Card> cardsInHand, List<Card> cardsPlayed, CardSuit trump, CardSuit firstCardSuit)
        {
            if (cardsPlayed.Count == 1)
            {
                return GetValidCardsForPlayNonTrumpFirst_OneCardPlayed(cardsInHand, cardsPlayed, trump, firstCardSuit);
            }

            if (cardsPlayed.Count > 1)
            {
                return GetValidCardsForPlayNonTrumpFirst_MoreCardsPlayed(cardsInHand, cardsPlayed, trump, firstCardSuit);
            }

            return cardsInHand;
        }

        private static List<Card> GetValidCardsForPlayNonTrumpFirst_OneCardPlayed(List<Card> cardsInHand, List<Card> cardsPlayed, CardSuit trump, CardSuit firstCardSuit)
        {
            if (cardsInHand.Any(c => c.Suit == firstCardSuit))
            {
                if (cardsInHand.Any(c => c.Suit == firstCardSuit && c.GetValueOrder(false) > cardsPlayed[0].GetValueOrder(false)))
                {
                    return cardsInHand.Where(c => c.Suit == firstCardSuit && c.GetValueOrder(false) > cardsPlayed[0].GetValueOrder(false)).ToList();
                }

                return cardsInHand.Where(c => c.Suit == firstCardSuit).ToList();
            }

            if (cardsInHand.Any(c => c.Suit == trump))
            {
                return cardsInHand.Where(c => c.Suit == trump).ToList();
            }

            return cardsInHand;
        }

        private static List<Card> GetValidCardsForPlayNonTrumpFirst_MoreCardsPlayed(List<Card> cardsInHand, List<Card> cardsPlayed, CardSuit trump, CardSuit firstCardSuit)
        {
            var strongestCard = cardsPlayed.Any(c => c.Suit == trump) 
                            ? GetStrongestTrumpCard(cardsPlayed.Where(c => c.Suit == trump).ToList(), trump) 
                            : cardsPlayed.Where(c => c.Suit == firstCardSuit).OrderByDescending(c => c.GetValueOrder(false)).First();

            if (strongestCard.Suit == trump)
            {
                if (cardsInHand.Any(c => c.Suit == firstCardSuit))
                {
                    return cardsInHand.Where(c => c.Suit == firstCardSuit).ToList();
                }

                if (cardsInHand.Any(c => c.Suit == trump))
                {
                    if (cardsInHand.Any(c => c.Suit == trump && c.GetValueOrder(true) > strongestCard.GetValueOrder(true)))
                    {
                        return cardsInHand.Where(c => c.Suit == trump && c.GetValueOrder(true) > strongestCard.GetValueOrder(true)).ToList();
                    }

                    return cardsInHand.Where(c => c.Suit == trump).ToList();
                }

                return cardsInHand;
            }

            if (strongestCard.Suit == firstCardSuit)
            {
                if (cardsInHand.Any(c => c.Suit == firstCardSuit))
                {
                    if (cardsInHand.Any(c => c.Suit == firstCardSuit && c.GetValueOrder(false) > strongestCard.GetValueOrder(false)))
                    {
                        return cardsInHand.Where(c => c.Suit == firstCardSuit && c.GetValueOrder(false) > strongestCard.GetValueOrder(false)).ToList();
                    }

                    return cardsInHand.Where(c => c.Suit == firstCardSuit).ToList();
                }

                if (cardsInHand.Any(c => c.Suit == trump))
                {
                    return cardsInHand.Where(c => c.Suit == trump).ToList();
                }

                return cardsInHand;
            }

            return cardsInHand;
        }

        private static Card GetStrongestTrumpCard(List<Card> cardsPlayed, CardSuit trump)
        {
            var strongestCard = cardsPlayed[0];
            if (cardsPlayed.Count > 1 && cardsPlayed[1].GetValueOrder(true) > strongestCard.GetValueOrder(true))
            {
                strongestCard = cardsPlayed[1];
            }

            if (cardsPlayed.Count > 2 && cardsPlayed[2].GetValueOrder(true) > strongestCard.GetValueOrder(true))
            {
                strongestCard = cardsPlayed[2];
            }

            return strongestCard;
        }
    }
}
