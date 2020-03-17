using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketToRideModelingPractice.Classes;
using TicketToRideModelingPractice.Enums;

namespace TicketToRideModelingPractice.Extensions
{
    public static class BoardRouteExtensions
    {
        public static List<BoardRoute> GetRoutesToCity(this List<BoardRoute> routes, City city)
        {
            var matchingRoute = routes.Where(x => x.Origin == city || x.Destination == city);
            if (matchingRoute.Any())
            {
                return matchingRoute.OrderBy(x => x.Length).ToList();
            }
            else return null;
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
