using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketToRideModelingPractice.Enums;
using TicketToRideModelingPractice.Extensions;

namespace TicketToRideModelingPractice.Classes
{
    public class Board
    {
        public BoardRouteCollection Routes { get; set; } = new BoardRouteCollection();

        public List<DestinationCard> DestinationCards { get; set; } = new List<DestinationCard>();

        public List<TrainCard> Deck { get; set; } = new List<TrainCard>();

        public List<TrainCard> DiscardPile { get; set; } = new List<TrainCard>();

        public List<TrainCard> ShownCards { get; set; } = new List<TrainCard>();

        public Board()
        {
            CreateBoardRoutes();
            CreateDestinationCards();
            CreateTrainCardDeck();
        }

        private void CreateBoardRoutes()
        {
            #region Create Routes
            Routes.AddRoute(City.Vancouver, City.Calgary, TrainColor.Grey, 3);
            Routes.AddRoute(City.Calgary, City.Winnipeg, TrainColor.White, 6);
            Routes.AddRoute(City.Winnipeg, City.SaultSaintMarie, TrainColor.Grey, 6);
            Routes.AddRoute(City.SaultSaintMarie, City.Montreal, TrainColor.Black, 5);
            Routes.AddRoute(City.Montreal, City.Boston, TrainColor.Grey, 2);
            Routes.AddRoute(City.Montreal, City.Boston, TrainColor.Grey, 2);
            Routes.AddRoute(City.Vancouver, City.Seattle, TrainColor.Grey, 1);
            Routes.AddRoute(City.Vancouver, City.Seattle, TrainColor.Grey, 1);
            Routes.AddRoute(City.Seattle, City.Calgary, TrainColor.Grey, 4);
            Routes.AddRoute(City.Calgary, City.Helena, TrainColor.Grey, 4);
            Routes.AddRoute(City.Helena, City.Winnipeg, TrainColor.Blue, 4);
            Routes.AddRoute(City.Winnipeg, City.Duluth, TrainColor.Black, 4);
            Routes.AddRoute(City.Duluth, City.SaultSaintMarie, TrainColor.Grey, 3);
            Routes.AddRoute(City.SaultSaintMarie, City.Toronto, TrainColor.Grey, 2);
            Routes.AddRoute(City.Toronto, City.Montreal, TrainColor.Grey, 3);
            Routes.AddRoute(City.Portland, City.Seattle, TrainColor.Grey, 1);
            Routes.AddRoute(City.Portland, City.Seattle, TrainColor.Grey, 1);
            Routes.AddRoute(City.Seattle, City.Helena, TrainColor.Yellow, 6);
            Routes.AddRoute(City.Helena, City.Duluth, TrainColor.Orange, 6);
            Routes.AddRoute(City.Duluth, City.Toronto, TrainColor.Purple, 6);
            Routes.AddRoute(City.Toronto, City.Pittsburgh, TrainColor.Grey, 2);
            Routes.AddRoute(City.Montreal, City.NewYork, TrainColor.Blue, 3);
            Routes.AddRoute(City.NewYork, City.Boston, TrainColor.Yellow, 2);
            Routes.AddRoute(City.NewYork, City.Boston, TrainColor.Red, 2);
            Routes.AddRoute(City.Portland, City.SanFrancisco, TrainColor.Green, 5);
            Routes.AddRoute(City.Portland, City.SanFrancisco, TrainColor.Purple, 5);
            Routes.AddRoute(City.Portland, City.SaltLakeCity, TrainColor.Blue, 5);
            Routes.AddRoute(City.SanFrancisco, City.SaltLakeCity, TrainColor.Orange, 5);
            Routes.AddRoute(City.SanFrancisco, City.SaltLakeCity, TrainColor.White, 5);
            Routes.AddRoute(City.SaltLakeCity, City.Helena, TrainColor.Purple, 3);
            Routes.AddRoute(City.Helena, City.Denver, TrainColor.Green, 4);
            Routes.AddRoute(City.SaltLakeCity, City.Denver, TrainColor.Red, 3);
            Routes.AddRoute(City.SaltLakeCity, City.Denver, TrainColor.Yellow, 3);
            Routes.AddRoute(City.Helena, City.Omaha, TrainColor.Red, 5);
            Routes.AddRoute(City.Omaha, City.Duluth, TrainColor.Grey, 2);
            Routes.AddRoute(City.Omaha, City.Duluth, TrainColor.Grey, 2);
            Routes.AddRoute(City.Duluth, City.Chicago, TrainColor.Red, 4);
            Routes.AddRoute(City.Omaha, City.Chicago, TrainColor.Blue, 4);
            Routes.AddRoute(City.Chicago, City.Toronto, TrainColor.White, 4);
            Routes.AddRoute(City.Chicago, City.Pittsburgh, TrainColor.Orange, 3);
            Routes.AddRoute(City.Chicago, City.Pittsburgh, TrainColor.Black, 3);
            Routes.AddRoute(City.Pittsburgh, City.NewYork, TrainColor.White, 2);
            Routes.AddRoute(City.Pittsburgh, City.NewYork, TrainColor.Green, 2);
            Routes.AddRoute(City.NewYork, City.Washington, TrainColor.Orange, 2);
            Routes.AddRoute(City.NewYork, City.Washington, TrainColor.Black, 2);
            Routes.AddRoute(City.SanFrancisco, City.LosAngeles, TrainColor.Yellow, 3);
            Routes.AddRoute(City.SanFrancisco, City.LosAngeles, TrainColor.Purple, 3);
            Routes.AddRoute(City.LosAngeles, City.LasVegas, TrainColor.Grey, 2);
            Routes.AddRoute(City.LasVegas, City.SaltLakeCity, TrainColor.Orange, 3);
            Routes.AddRoute(City.LosAngeles, City.Phoenix, TrainColor.Grey, 3);
            Routes.AddRoute(City.LosAngeles, City.ElPaso, TrainColor.Black, 6);
            Routes.AddRoute(City.Phoenix, City.Denver, TrainColor.White, 5);
            Routes.AddRoute(City.Phoenix, City.SantaFe, TrainColor.Grey, 3);
            Routes.AddRoute(City.Phoenix, City.ElPaso, TrainColor.Grey, 3);
            Routes.AddRoute(City.ElPaso, City.SantaFe, TrainColor.Grey, 2);
            Routes.AddRoute(City.SantaFe, City.Denver, TrainColor.Grey, 2);
            Routes.AddRoute(City.Denver, City.KansasCity, TrainColor.Black, 4);
            Routes.AddRoute(City.Denver, City.KansasCity, TrainColor.Orange, 4);
            Routes.AddRoute(City.Omaha, City.Duluth, TrainColor.Grey, 2);
            Routes.AddRoute(City.Omaha, City.Duluth, TrainColor.Grey, 2);
            Routes.AddRoute(City.Omaha, City.KansasCity, TrainColor.Grey, 1);
            Routes.AddRoute(City.Omaha, City.KansasCity, TrainColor.Grey, 1);
            Routes.AddRoute(City.Denver, City.OklahomaCity, TrainColor.Red, 4);
            Routes.AddRoute(City.SantaFe, City.OklahomaCity, TrainColor.Blue, 2);
            Routes.AddRoute(City.ElPaso, City.OklahomaCity, TrainColor.Yellow, 5);
            Routes.AddRoute(City.ElPaso, City.Dallas, TrainColor.Red, 4);
            Routes.AddRoute(City.ElPaso, City.Houston, TrainColor.Green, 6);
            Routes.AddRoute(City.KansasCity, City.SaintLouis, TrainColor.Blue, 2);
            Routes.AddRoute(City.KansasCity, City.SaintLouis, TrainColor.Purple, 2);
            Routes.AddRoute(City.SaintLouis, City.Chicago, TrainColor.Green, 2);
            Routes.AddRoute(City.SaintLouis, City.Chicago, TrainColor.White, 2);
            Routes.AddRoute(City.SaintLouis, City.Pittsburgh, TrainColor.Green, 5);
            Routes.AddRoute(City.Pittsburgh, City.Washington, TrainColor.Grey, 2);
            Routes.AddRoute(City.SaintLouis, City.Nashville, TrainColor.Grey, 2);
            Routes.AddRoute(City.KansasCity, City.OklahomaCity, TrainColor.Grey, 2);
            Routes.AddRoute(City.KansasCity, City.OklahomaCity, TrainColor.Grey, 2);
            Routes.AddRoute(City.OklahomaCity, City.Dallas, TrainColor.Grey, 2);
            Routes.AddRoute(City.OklahomaCity, City.Dallas, TrainColor.Grey, 2);
            Routes.AddRoute(City.Dallas, City.Houston, TrainColor.Grey, 1);
            Routes.AddRoute(City.Dallas, City.Houston, TrainColor.Grey, 1);
            Routes.AddRoute(City.OklahomaCity, City.LittleRock, TrainColor.Grey, 2);
            Routes.AddRoute(City.LittleRock, City.SaintLouis, TrainColor.Grey, 2);
            Routes.AddRoute(City.SaintLouis, City.Nashville, TrainColor.Grey, 2);
            Routes.AddRoute(City.Nashville, City.Pittsburgh, TrainColor.Yellow, 4);
            Routes.AddRoute(City.Pittsburgh, City.Raleigh, TrainColor.Grey, 2);
            Routes.AddRoute(City.Nashville, City.Raleigh, TrainColor.Black, 3);
            Routes.AddRoute(City.Raleigh, City.Washington, TrainColor.Grey, 2);
            Routes.AddRoute(City.Raleigh, City.Washington, TrainColor.Grey, 2);
            Routes.AddRoute(City.Raleigh, City.Charleston, TrainColor.Grey, 2);
            Routes.AddRoute(City.Dallas, City.LittleRock, TrainColor.Grey, 2);
            Routes.AddRoute(City.LittleRock, City.Nashville, TrainColor.White, 3);
            Routes.AddRoute(City.Nashville, City.Atlanta, TrainColor.Grey, 1);
            Routes.AddRoute(City.Atlanta, City.Raleigh, TrainColor.Grey, 2);
            Routes.AddRoute(City.Atlanta, City.Raleigh, TrainColor.Grey, 2);
            Routes.AddRoute(City.Atlanta, City.Charleston, TrainColor.Grey, 2);
            Routes.AddRoute(City.LittleRock, City.NewOrleans, TrainColor.Green, 3);
            Routes.AddRoute(City.Houston, City.NewOrleans, TrainColor.Grey, 2);
            Routes.AddRoute(City.NewOrleans, City.Atlanta, TrainColor.Yellow, 4);
            Routes.AddRoute(City.NewOrleans, City.Atlanta, TrainColor.Orange, 4);
            Routes.AddRoute(City.NewOrleans, City.Miami, TrainColor.Red, 6);
            Routes.AddRoute(City.Atlanta, City.Miami, TrainColor.Blue, 5);
            Routes.AddRoute(City.Charleston, City.Miami, TrainColor.Purple, 4);
            #endregion
        }

        private void CreateDestinationCards()
        {
            DestinationCards.Add(new DestinationCard(City.NewYork, City.Atlanta, 6));
            DestinationCards.Add(new DestinationCard(City.Winnipeg, City.LittleRock, 11));
            DestinationCards.Add(new DestinationCard(City.Boston, City.Miami, 12));
            DestinationCards.Add(new DestinationCard(City.LosAngeles, City.Chicago, 16));
            DestinationCards.Add(new DestinationCard(City.Montreal, City.Atlanta, 9));
            DestinationCards.Add(new DestinationCard(City.Seattle, City.LosAngeles, 9));
            DestinationCards.Add(new DestinationCard(City.KansasCity, City.Houston, 5));
            DestinationCards.Add(new DestinationCard(City.Chicago, City.NewOrleans, 7));
            DestinationCards.Add(new DestinationCard(City.Seattle, City.NewYork, 22));
            DestinationCards.Add(new DestinationCard(City.Portland, City.Nashville, 17));
            DestinationCards.Add(new DestinationCard(City.SaultSaintMarie, City.OklahomaCity, 9));
            DestinationCards.Add(new DestinationCard(City.Vancouver, City.SantaFe, 13));
            DestinationCards.Add(new DestinationCard(City.SanFrancisco, City.Atlanta, 17));
            DestinationCards.Add(new DestinationCard(City.Vancouver, City.Montreal, 20));
            DestinationCards.Add(new DestinationCard(City.Montreal, City.NewOrleans, 13));
            DestinationCards.Add(new DestinationCard(City.LosAngeles, City.NewYork, 21));
            DestinationCards.Add(new DestinationCard(City.Calgary, City.SaltLakeCity, 7));
            DestinationCards.Add(new DestinationCard(City.Denver, City.Pittsburgh, 11));
            DestinationCards.Add(new DestinationCard(City.Helena, City.LosAngeles, 8));
            DestinationCards.Add(new DestinationCard(City.Calgary, City.Phoenix, 13));
            DestinationCards.Add(new DestinationCard(City.Chicago, City.SantaFe, 9));
            DestinationCards.Add(new DestinationCard(City.Toronto, City.Miami, 10)); ;
            DestinationCards.Add(new DestinationCard(City.Dallas, City.NewYork, 11));
            DestinationCards.Add(new DestinationCard(City.Duluth, City.Houston, 8));
            DestinationCards.Add(new DestinationCard(City.SaultSaintMarie, City.Nashville, 8));
            DestinationCards.Add(new DestinationCard(City.Duluth, City.ElPaso, 10));
            DestinationCards.Add(new DestinationCard(City.Winnipeg, City.Houston, 12));
            DestinationCards.Add(new DestinationCard(City.Denver, City.ElPaso, 4));
            DestinationCards.Add(new DestinationCard(City.LosAngeles, City.Miami, 20));
            DestinationCards.Add(new DestinationCard(City.Portland, City.Phoenix, 11));

            DestinationCards = DestinationCards.Shuffle();
        }

        private void CreateTrainCardDeck()
        {
            var deck = new List<TrainCard>();
            deck.AddRange(CreateSingleColorCollection(TrainColor.Red, 12));
            deck.AddRange(CreateSingleColorCollection(TrainColor.Purple, 12));
            deck.AddRange(CreateSingleColorCollection(TrainColor.Blue, 12));
            deck.AddRange(CreateSingleColorCollection(TrainColor.Green, 12));
            deck.AddRange(CreateSingleColorCollection(TrainColor.Yellow, 12));
            deck.AddRange(CreateSingleColorCollection(TrainColor.Orange, 12));
            deck.AddRange(CreateSingleColorCollection(TrainColor.Black, 12));
            deck.AddRange(CreateSingleColorCollection(TrainColor.White, 12));
            deck.AddRange(CreateSingleColorCollection(TrainColor.Locomotive, 14));

            deck.Shuffle();

            this.Deck = deck;
        }

        private List<TrainCard> CreateSingleColorCollection(TrainColor color, int count)
        {
            List<TrainCard> cards = new List<TrainCard>();
            for(int i = 0; i < count; i++)
            {
                cards.Add(new TrainCard() { Color = color });
            }
            return cards;
        }

        public void FlipShownCards()
        {
            while (ShownCards.Count < 5)
            {
                var newCard = Deck.Pop(1);
                ShownCards.AddRange(newCard);
            }
        }

        public void PopulateShownCards()
        {
            if (Deck.Count < 10)
            {
                //Shuffle the discard pile into the deck
                var allCards = DiscardPile;
                allCards.AddRange(Deck.Pop(Deck.Count));
                
                allCards.Shuffle();

                Deck = allCards;
                DiscardPile = new List<TrainCard>();
            }

            FlipShownCards();

            var locomotiveCount = ShownCards.Where(x => x.Color == TrainColor.Locomotive).Count();
            while(locomotiveCount >= 3)
            {
                Console.WriteLine("Shown cards has 3 or more locomotives! Burning the shown cards.");

                //Discard the shown cards
                DiscardPile.AddRange(ShownCards);
                ShownCards = new List<TrainCard>();

                //Add a new set of shown cards
                FlipShownCards();
                locomotiveCount = ShownCards.Where(x => x.Color == TrainColor.Locomotive).Count();
            }
        }
    }
}
