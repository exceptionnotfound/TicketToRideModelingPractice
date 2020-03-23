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

        public static List<DestinationCard> Shuffle(this List<DestinationCard> cards)
        {
            Random r = new Random();
            //Step 1: For each unshuffled item in the collection
            for (int n = cards.Count - 1; n > 0; --n)
            {
                //Step 2: Randomly pick an item which has not been shuffled
                int k = r.Next(n + 1);

                //Step 3: Swap the selected item with the last "unstruck" letter in the collection
                DestinationCard temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
            }

            return cards;
        }

        public static List<TrainCard> Shuffle(this List<TrainCard> cards)
        {
            Random r = new Random();
            //Step 1: For each unshuffled item in the collection
            for (int n = cards.Count - 1; n > 0; --n)
            {
                //Step 2: Randomly pick an item which has not been shuffled
                int k = r.Next(n + 1);

                //Step 3: Swap the selected item with the last "unstruck" letter in the collection
                TrainCard temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
            }

            return cards;
        }
    }
}
