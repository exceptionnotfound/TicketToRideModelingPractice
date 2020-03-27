using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketToRideModelingPractice.Europe.Enums;
using TicketToRideModelingPractice.Europe.Extensions;

namespace TicketToRideModelingPractice.Europe.Classes
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
            Routes.AddRoute(City.Lisboa, City.Madrid, TrainColor.Purple, 3);
            Routes.AddRoute(City.Lisboa, City.Cadiz, TrainColor.Blue, 2);
            Routes.AddRoute(City.Cadiz, City.Madrid, TrainColor.Orange, 3);
            Routes.AddTunnel(City.Madrid, City.Pamplona, TrainColor.Black, 3);
            Routes.AddTunnel(City.Madrid, City.Pamplona, TrainColor.White, 3);
            Routes.AddRoute(City.Madrid, City.Barcelona, TrainColor.Yellow, 2);
            Routes.AddTunnel(City.Pamplona, City.Barcelona, TrainColor.Grey, 2);
            Routes.AddRoute(City.Brest, City.Dieppe, TrainColor.Orange, 2);
            Routes.AddRoute(City.Brest, City.Paris, TrainColor.Black, 3);
            Routes.AddRoute(City.Brest, City.Pamplona, TrainColor.Purple, 4);
            Routes.AddRoute(City.Dieppe, City.Paris, TrainColor.Purple, 1);
            Routes.AddRoute(City.Edinburgh, City.London, TrainColor.Black, 4);
            Routes.AddRoute(City.Edinburgh, City.London, TrainColor.Orange, 4);
            Routes.AddFerry(City.London, City.Dieppe, 2, 1);
            Routes.AddFerry(City.London, City.Dieppe, 2, 1);
            Routes.AddRoute(City.Pamplona, City.Paris, TrainColor.Blue, 4);
            Routes.AddRoute(City.Pamplona, City.Paris, TrainColor.Green, 4);
            Routes.AddRoute(City.Pamplona, City.Marseille, TrainColor.Red, 4);
            Routes.AddRoute(City.Barcelona, City.Marseille, TrainColor.Grey, 4);
            Routes.AddFerry(City.London, City.Amsterdam, 2, 2);
            Routes.AddRoute(City.Dieppe, City.Bruxelles, TrainColor.Green, 2);
            Routes.AddRoute(City.Bruxelles, City.Amsterdam, TrainColor.Black, 1);
            Routes.AddRoute(City.Paris, City.Bruxelles, TrainColor.Yellow, 2);
            Routes.AddRoute(City.Paris, City.Bruxelles, TrainColor.Red, 2);
            Routes.AddRoute(City.Amsterdam, City.Essen, TrainColor.Yellow, 3);
            Routes.AddRoute(City.Amsterdam, City.Frankfurt, TrainColor.White, 2);
            Routes.AddRoute(City.Bruxelles, City.Frankfurt, TrainColor.Blue, 2);
            Routes.AddRoute(City.Paris, City.Frankfurt, TrainColor.White, 3);
            Routes.AddRoute(City.Paris, City.Frankfurt, TrainColor.Orange, 3);
            Routes.AddTunnel(City.Paris, City.Zurich, TrainColor.Grey, 3);
            Routes.AddRoute(City.Paris, City.Marseille, TrainColor.Grey, 4);
            Routes.AddTunnel(City.Marseille, City.Zurich, TrainColor.Purple, 2);
            Routes.AddFerry(City.Essen, City.Kobenhavn, 3, 1);
            Routes.AddFerry(City.Essen, City.Kobenhavn, 3, 1);
            Routes.AddRoute(City.Frankfurt, City.Essen, TrainColor.Green, 2);
            Routes.AddRoute(City.Essen, City.Berlin, TrainColor.Blue, 2);
            Routes.AddRoute(City.Frankfurt, City.Berlin, TrainColor.Black, 3);
            Routes.AddRoute(City.Frankfurt, City.Berlin, TrainColor.Red, 3);
            Routes.AddRoute(City.Frankfurt, City.Munchen, TrainColor.Purple, 2);
            Routes.AddTunnel(City.Zurich, City.Munchen, TrainColor.Yellow, 2);
            Routes.AddTunnel(City.Zurich, City.Venezia, TrainColor.Green, 2);
            Routes.AddTunnel(City.Marseille, City.Roma, TrainColor.Grey, 4);
            Routes.AddRoute(City.Munchen, City.Wien, TrainColor.Orange, 3);
            Routes.AddTunnel(City.Munchen, City.Venezia, TrainColor.Blue, 2);
            Routes.AddRoute(City.Venezia, City.Zagrab, TrainColor.Grey, 2);
            Routes.AddRoute(City.Venezia, City.Roma, TrainColor.Black, 2);
            Routes.AddRoute(City.Roma, City.Brindisi, TrainColor.White, 2);
            Routes.AddFerry(City.Roma, City.Palermo, 4, 1);
            Routes.AddRoute(City.Kobenhavn, City.Stockholm, TrainColor.Yellow, 3);
            Routes.AddRoute(City.Kobenhavn, City.Stockholm, TrainColor.White, 3);
            Routes.AddRoute(City.Berlin, City.Danzig, TrainColor.Grey, 4);
            Routes.AddRoute(City.Berlin, City.Warzawa, TrainColor.Purple, 4);
            Routes.AddRoute(City.Berlin, City.Warzawa, TrainColor.Yellow, 4);
            Routes.AddRoute(City.Berlin, City.Wien, TrainColor.Green, 3);
            Routes.AddRoute(City.Zagrab, City.Wien, TrainColor.Grey, 2);
            Routes.AddRoute(City.Zagrab, City.Sarajevo, TrainColor.Red, 3);
            Routes.AddRoute(City.Wien, City.Budapest, TrainColor.Red, 1);
            Routes.AddRoute(City.Wien, City.Budapest, TrainColor.White, 1);
            Routes.AddRoute(City.Zagrab, City.Budapest, TrainColor.Orange, 2);
            Routes.AddFerry(City.Palermo, City.Brindisi, 3, 1);
            Routes.AddFerry(City.Brindisi, City.Athina, 4, 1);
            Routes.AddRoute(City.Danzig, City.Riga, TrainColor.Black, 3);
            Routes.AddRoute(City.Danzig, City.Warzawa, TrainColor.Grey, 2);
            Routes.AddRoute(City.Wien, City.Warzawa, TrainColor.Blue, 5);
            Routes.AddRoute(City.Budapest, City.Sarajevo, TrainColor.Purple, 3);
            Routes.AddTunnel(City.Sarajevo, City.Sofia, TrainColor.Grey, 2);
            Routes.AddRoute(City.Sarajevo, City.Athina, TrainColor.Green, 4);
            Routes.AddTunnel(City.Budapest, City.Kyiv, TrainColor.Grey, 6);
            Routes.AddTunnel(City.Budapest, City.Bucuresti, TrainColor.Grey, 4);
            Routes.AddRoute(City.Athina, City.Sofia, TrainColor.Purple, 3);
            Routes.AddFerry(City.Palermo, City.Smyrna, 6, 2);
            Routes.AddRoute(City.Warzawa, City.Wilno, TrainColor.Red, 3);
            Routes.AddRoute(City.Riga, City.Wilno, TrainColor.Green, 4);
            Routes.AddRoute(City.Riga, City.Petrograd, TrainColor.Grey, 4);
            Routes.AddTunnel(City.Stockholm, City.Petrograd, TrainColor.Grey, 8);
            Routes.AddRoute(City.Warzawa, City.Kyiv, TrainColor.Grey, 4);
            Routes.AddTunnel(City.Sofia, City.Bucuresti, TrainColor.Grey, 2);
            Routes.AddRoute(City.Sofia, City.Constantinople, TrainColor.Blue, 3);
            Routes.AddFerry(City.Athina, City.Smyrna, 2, 1);
            Routes.AddRoute(City.Wilno, City.Smolensk, TrainColor.Yellow, 3);
            Routes.AddRoute(City.Wilno, City.Kyiv, TrainColor.Grey, 2);
            Routes.AddRoute(City.Bucuresti, City.Kyiv, TrainColor.Grey, 4);
            Routes.AddRoute(City.Bucuresti, City.Sevastapol, TrainColor.White, 4);
            Routes.AddRoute(City.Bucuresti, City.Constantinople, TrainColor.Yellow, 3);
            Routes.AddTunnel(City.Smyrna, City.Constantinople, TrainColor.Grey, 2);
            Routes.AddTunnel(City.Smyrna, City.Angora, TrainColor.Orange, 3);
            Routes.AddRoute(City.Kyiv, City.Smolensk, TrainColor.Red, 3);
            Routes.AddRoute(City.Kyiv, City.Kharkov, TrainColor.Grey, 4);
            Routes.AddFerry(City.Constantinople, City.Sevastapol, 4, 2);
            Routes.AddTunnel(City.Constantinople, City.Angora, TrainColor.Grey, 2);
            Routes.AddRoute(City.Petrograd, City.Moskva, TrainColor.White, 4);
            Routes.AddRoute(City.Smolensk, City.Moskva, TrainColor.Orange, 2);
            Routes.AddRoute(City.Moskva, City.Kharkov, TrainColor.Grey, 4);
            Routes.AddRoute(City.Sevastapol, City.Rostov, TrainColor.Grey, 4);
            Routes.AddFerry(City.Sevastapol, City.Sochi, 2, 1);
            Routes.AddFerry(City.Sevastapol, City.Erzurum, 4, 2);
            Routes.AddRoute(City.Rostov, City.Sochi, TrainColor.Grey, 2);
            Routes.AddRoute(City.Angora, City.Erzurum, TrainColor.Black, 3);
            Routes.AddTunnel(City.Erzurum, City.Sochi, TrainColor.Red, 3);
            #endregion
        }

        private void CreateDestinationCards()
        {
            DestinationCards.Add(City.Athina, City.Angora, 5);
            DestinationCards.Add(City.Budapest, City.Sofia, 5);
            DestinationCards.Add(City.Frankfurt, City.Kobenhavn, 5);
            DestinationCards.Add(City.Rostov, City.Erzurum, 5);
            DestinationCards.Add(City.Sofia, City.Smyrna, 5);
            DestinationCards.Add(City.Kyiv, City.Petrograd, 5);
            DestinationCards.Add(City.Zurich, City.Brindisi, 6);
            DestinationCards.Add(City.Zurich, City.Budapest, 6);
            DestinationCards.Add(City.Warzawa, City.Smolensk, 6);
            DestinationCards.Add(City.Zagrab, City.Brindisi, 6);
            DestinationCards.Add(City.Paris, City.Zagrab, 7);
            DestinationCards.Add(City.Brest, City.Marseille, 7);
            DestinationCards.Add(City.London, City.Berlin, 7);
            DestinationCards.Add(City.Edinburgh, City.Paris, 7);
            DestinationCards.Add(City.Amsterdam, City.Pamplona, 7);
            DestinationCards.Add(City.Roma, City.Smyrna, 8);
            DestinationCards.Add(City.Palermo, City.Constantinople, 8);
            DestinationCards.Add(City.Sarajevo, City.Sevastapol, 8);
            DestinationCards.Add(City.Madrid, City.Dieppe, 8);
            DestinationCards.Add(City.Barcelona, City.Bruxelles, 8);
            DestinationCards.Add(City.Paris, City.Wien, 8);
            DestinationCards.Add(City.Barcelona, City.Munchen, 8);
            DestinationCards.Add(City.Brest, City.Venezia, 8);
            DestinationCards.Add(City.Smolensk, City.Rostov, 8);
            DestinationCards.Add(City.Marseille, City.Essen, 8);
            DestinationCards.Add(City.Kyiv, City.Sochi, 8);
            DestinationCards.Add(City.Madrid, City.Zurich, 8);
            DestinationCards.Add(City.Berlin, City.Bucuresti, 8);
            DestinationCards.Add(City.Berlin, City.Roma, 9);
            DestinationCards.Add(City.Bruxelles, City.Danzig, 9);
            DestinationCards.Add(City.Angora, City.Kharkov, 10);
            DestinationCards.Add(City.Riga, City.Bucuresti, 10);
            DestinationCards.Add(City.Essen, City.Kyiv, 10);
            DestinationCards.Add(City.Venezia, City.Constantinople, 10);
            DestinationCards.Add(City.London, City.Wien, 10);
            DestinationCards.Add(City.Athina, City.Wilno, 11);
            DestinationCards.Add(City.Stockholm, City.Wien, 11);
            DestinationCards.Add(City.Berlin, City.Moskva, 12);
            DestinationCards.Add(City.Amsterdam, City.Wilno, 12);
            DestinationCards.Add(City.Frankfurt, City.Smolensk, 13);

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
            for (int i = 0; i < count; i++)
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
            while (locomotiveCount >= 3)
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
