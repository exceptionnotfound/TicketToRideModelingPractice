using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketToRideModelingPractice.Classes;
using TicketToRideModelingPractice.Enums;

namespace TicketToRideModelingPractice.Extensions
{
    public static class TrainCardExtensions
    {
        public static TrainCard Pop(this List<TrainCard> cards, TrainColor color)
        {
            if(cards.Any(x=>x.Color == color))
            {
                var selectedCard = cards.First(x => x.Color == color);
                cards.Remove(selectedCard);
                return selectedCard;
            }
            return null;
        }

        public static List<TrainCard> GetMatching(this List<TrainCard> cards, TrainColor color, int count)
        {
            if(cards.Where(x=>x.Color == color).Count() >= count)
            {
                var selectedCards = cards.Where(x => x.Color == color).Take(count).ToList();
                foreach(var card in selectedCards)
                {
                    cards.Remove(card);
                }

                return selectedCards;
            }

            return new List<TrainCard>();
        }

        public static List<TrainCard> Pop(this List<TrainCard> cards, int count)
        {
            List<TrainCard> returnCards = new List<TrainCard>();
            for(int i = 0; i < count; i++)
            {
                var selectedCard = cards[0];
                cards.RemoveAt(0);
                returnCards.Add(selectedCard);
            }

            return returnCards;
        }

        public static List<DestinationCard> Pop(this List<DestinationCard> cards, int count)
        {
            List<DestinationCard> returnCards = new List<DestinationCard>();
            for (int i = 0; i < count; i++)
            {
                var selectedCard = cards[0];
                cards.RemoveAt(0);
                returnCards.Add(selectedCard);
                if (!cards.Any()) break;
            }

            return returnCards;
        }

        public static TrainColor GetMostPopularColor(this List<TrainCard> cards)
        {
            if (!cards.Any())
                return TrainColor.Locomotive;

            return cards.GroupBy(x => x.Color)
                        .Select(group =>
                        new
                        {
                            Color = group.Key,
                            Count = group.Count()
                        })
                        .OrderByDescending(x => x.Count)
                        .First()
                        .Color;
        }
    }
}
