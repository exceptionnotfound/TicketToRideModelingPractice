using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketToRideModelingPractice.Enums;
using TicketToRideModelingPractice.Extensions;

namespace TicketToRideModelingPractice.Classes
{
    public class Player
    {
        //The player's name
        public string Name { get; set; }

        //The player's current collection of destination cards.
        public List<DestinationCard> DestinationCards { get; set; } = new List<DestinationCard>();

        //The routes the player wants to claim at any given time.
        public List<BoardRoute> TargetedRoutes { get; set; } = new List<BoardRoute>();

        //All the cities this Player has already connected.
        public List<City> ConnectedCities { get; set; } = new List<City>();

        //The player's color (e.g. red, blue, black, green, or yellow)
        public PlayerColor Color { get; set; }

        //The Player's current collection of train cards.
        public List<TrainCard> Hand { get; set; } = new List<TrainCard>();

        //When one player has 2 or less train cars the remaining, the final turn begins.
        public int RemainingTrainCars { get; set; } = 48;

        //The train card colors this Player wants to draw.
        public List<TrainColor> DesiredColors { get; set; } = new List<TrainColor>();

        //A reference to the game board
        public Board Board { get; set; }

        public Player(string name, PlayerColor color, Board board)
        {
            Name = name;
            DestinationCards = board.DestinationCards.Pop(3).ToList();
            Color = color;
            Board = board;

            CalculateTargetedRoutes();
        }

    public List<BoardRoute> CalculateTargetedRoutes(DestinationCard card)
    {
        var allRoutes = new List<BoardRoute>();

        //Are the origin and destination already connected?
        if(Board.Routes.IsAlreadyConnected(card.Origin, card.Destination, Color))
        {
            return allRoutes;
        }
        Board.Routes.AlreadyCheckedCities.Clear();

        //If the cities aren't already connected, attempt to connect them from something we've already connected.
        foreach(var city in ConnectedCities)
        {
            var foundDestinationRoutes = Board.Routes.FindIdealUnclaimedRoute(city, card.Destination);
            if(foundDestinationRoutes.Any())
            {
                allRoutes.AddRange(foundDestinationRoutes);
                break;
            }

            var foundOriginRoutes = Board.Routes.FindIdealUnclaimedRoute(card.Origin, city);
            if(foundOriginRoutes.Any())
            {
                allRoutes.AddRange(foundOriginRoutes);
                break;
            }
        }

        //If we can't connect them from something we have already connected, can we make a brand new connection?
        allRoutes = Board.Routes.FindIdealUnclaimedRoute(card.Origin, card.Destination);

        var routesToRemove = new List<BoardRoute>();
        foreach(var route in allRoutes)
        {
            var matchingRoutes = Board.Routes.Routes.Where(x => x.Length == route.Length && x.IsOccupied && x.OccupyingPlayerColor == Color &&
                                                            ((x.Origin == route.Origin && x.Destination == route.Destination)
                                                            || x.Origin == route.Destination && x.Destination == route.Origin));

            if (matchingRoutes.Any())
                routesToRemove.Add(route);
        }

        foreach(var route in routesToRemove)
        {
            allRoutes.Remove(route);
        }

        return allRoutes;
    }

        public void CalculateTargetedRoutes()
        {
            var allRoutes = new List<BoardRoute>();

            var highestCards = DestinationCards.OrderBy(x => x.PointValue).ToList();

            foreach(var destCard in highestCards)
            {
                var matchingRoutes = CalculateTargetedRoutes(destCard);
                if (matchingRoutes.Any())
                {
                    allRoutes.AddRange(matchingRoutes);
                    break;
                }
            }

            TargetedRoutes = allRoutes.GroupBy(x => new { x.Origin, x.Destination, x.Color, x.Length })
                                      .Select(group => new
                                      {
                                          Metric = group.Key,
                                          Count = group.Count()
                                      }).OrderByDescending(x => x.Count)
                                        .ThenByDescending(x => x.Metric.Length)
                                        .Take(5)
                                        .Select(x => new BoardRoute(x.Metric.Origin, x.Metric.Destination, x.Metric.Color, x.Metric.Length))
                                        .ToList();


            DesiredColors = TargetedRoutes.Select(x => x.Color)
                                          .Distinct()
                                          .ToList();

            if(!DesiredColors.Any())
            {
                //The player should target what they have the most of

                var color = Hand.GroupBy(x => x.Color)
                             .Select(group =>
                             new
                             {
                                 Color = group.Key,
                                 Count = group.Count()
                             })
                             .OrderByDescending(x => x.Count)
                             .Select(x => x.Color)
                             .First();

                DesiredColors.Add(color);
            }
        }

        public void OutputPlayerSummary()
        {
            //Player name and color
            Console.WriteLine("Name: " + Name + ", Color: " + Color.ToString() + ", Remaining Trains: " + RemainingTrainCars.ToString());

            //For each destination card, output them
            Console.WriteLine("Destination Cards: ");
            foreach(var destCard in DestinationCards)
            {
                Console.WriteLine(destCard.Origin.ToString() + " to " + destCard.Destination.ToString() + ", " + destCard.PointValue + " points");
            }

            Console.WriteLine("Desired Routes:");

            foreach(var desiredRoute in TargetedRoutes)
            {
                Console.WriteLine(desiredRoute.Origin + " to " + desiredRoute.Destination + " (" + desiredRoute.Color + "), " + desiredRoute.PointValue + " points");
            }

            Console.WriteLine("Desired Colors:");

            string colorList = "";
            foreach(var color in DesiredColors)
            {
                colorList += color + ", ";
            }

            if(colorList.Contains(","))
                colorList = colorList.Remove(colorList.LastIndexOf(","));
            Console.WriteLine(colorList);

            Console.WriteLine("Current Hand:");
            var groups = Hand.GroupBy(x => x.Color)
                             .Select(x => new
                             {
                                 Color = x.Key,
                                 Count = x.Count()
                             })
                             .OrderByDescending(x => x.Count);
            foreach(var cards in groups)
            {
                Console.WriteLine(cards.Color + " x " + cards.Count);
            }

            Console.WriteLine("");
        }

        public void TakeTurn()
        {
            CalculateTargetedRoutes();

            //The calculation for Desired Routes only returned routes that can still be claimed.
            //If there are no desired routes, then all routes the player wants are already claimed
            //(whether by this player or someone else).
            //Therefore, if there are no desired routes, draw new destination cards
            if (!TargetedRoutes.Any())
            {
                if (!Board.DestinationCards.Any())
                {
                    Console.WriteLine("No destination cards remain! " + Name + " must do something else!");
                }
                else if (DestinationCards.Count < 5)
                {
                    DrawDestinationCards();
                    return;
                }
            }

            //If the player can claim a route they desire, they will do so immediately.
            var hasClaimed = TryClaimRoute();

            if (hasClaimed)
                return;

            //We now have a problem. It is possible for a player to have a lot of train cards. 
            //So, let's have them claim the longest route they can claim
            //with the cards they have available, if they have more than 24 cards.
            if (Hand.Count >= 24)
            {
                ClaimLongestUnclaimedRoute();
            }
            else
            {
                DrawCards();
            }
        }

        //If the player cannot draw destination cards or claim a route, they need to draw train cards.
        //Here's the logic for this:
        //First, the player looks at the available draw cards. 
        //If two of them are their desired colors, they take them both.
        //If only one is a desired color, they will take that and one from the deck.
        //If there are no desired colors showing, but there is a locomotive, they will take that.
        //If no desired colors and no locomotives are showing, they will take two off the deck.
        public void DrawCards()
        {
            //If the player wants a grey route, they will also be able to take whatever they have the most of already
            if (DesiredColors.Contains(TrainColor.Grey))
            {
                var mostPopularColor = Hand.GetMostPopularColor(DesiredColors);
                DesiredColors.Add(mostPopularColor);
                DesiredColors.Remove(TrainColor.Grey);
            }

            //Check the desired colors against the shown cards
            var shownColors = Board.ShownCards.Select(x=>x.Color);
            var matchingColors = DesiredColors.Intersect(shownColors).ToList();

            var desiredCards = Board.ShownCards.Where(x => DesiredColors.Contains(x.Color));

            if (matchingColors.Count() >= 2)
            {
                //Take the cards and add them to the hand
                var cards = desiredCards.Take(2).ToList();
                foreach(var card in cards)
                {
                    Console.WriteLine(Name + " takes the shown " + card.Color + " card.");
                    Board.ShownCards.Remove(card);
                    Hand.Add(card);
                }
            }
            else if (matchingColors.Count() == 1)
            {
                //Take the shown color
                var card = desiredCards.First();
                Board.ShownCards.Remove(card);
                Hand.Add(card);

                Console.WriteLine(Name + " takes the shown " + card.Color + " card.");

                //Also take one from the deck
                var deckCard = Board.Deck.Pop(1).First();
                Hand.Add(deckCard);
                Console.WriteLine(Name + " also draws one card from the deck.");
            }
            else if (matchingColors.Count() == 0 && Board.ShownCards.Any(x=>x.Color == TrainColor.Locomotive))
            {
                Console.WriteLine(Name + " takes the shown locomotive.");
                //Take the locomotive card
                var card = Board.ShownCards.First(x => x.Color == TrainColor.Locomotive);
                Board.ShownCards.Remove(card);
                Hand.Add(card);
            }
            else
            {
                Console.WriteLine(Name + " draws two cards from the deck.");
                //Take two cards from the deck
                var cards = Board.Deck.Pop(2);
                Hand.AddRange(cards);
            }

            Board.PopulateShownCards();
        }

        public void DrawDestinationCards()
        {
            var tempDestinationCards = (Board.DestinationCards.Pop(3)).OrderByDescending(x => x.PointValue);

            //For each of these cards, keep only the one that's either complete or is completable.

            List<DestinationCard> discardCards = new List<DestinationCard>();
            List<DestinationCard> keptCards = new List<DestinationCard>();
            foreach(var card in tempDestinationCards)
            {
                //Keep cards that are already connected
                if (Board.Routes.IsAlreadyConnected(card.Origin, card.Destination, Color))
                {
                    keptCards.Add(card);
                    continue;
                }

                Board.Routes.AlreadyCheckedCities.Clear();

                var possibleRoutes = CalculateTargetedRoutes(card);
                if (!possibleRoutes.Any())
                    discardCards.Add(card);
                else
                    keptCards.Add(card);
            }

            //If there are no kept cards, the player must keep at least one, so keep the one with the lowest point value.
            if(!keptCards.Any())
            {
                discardCards = discardCards.OrderBy(x => x.PointValue).ToList();
                keptCards.AddRange(discardCards.Pop(1));
            }

            if(discardCards.Any())
            {
                //Return the discarded cards to the destination card pile
                Board.DestinationCards.AddRange(discardCards);
            }

            DestinationCards.AddRange(keptCards);

            CalculateTargetedRoutes();

            Console.WriteLine(Name + " draws new destination cards!");

            return;
        }

        public bool TryClaimRoute()
        {
            //How do we know if the player can claim a route they desire?
            //For each of the desired routes, loop through and see if the player has sufficient cards to claim the route.
            foreach(var route in TargetedRoutes)
            {
                //How many cards do we need for this route?
                var cardCount = route.Length;

                var selectedColor = route.Color;

                //If the player is targeting a Grey route, they can use any color
                //as long as it's not their currently desired color.
                if (route.Color == TrainColor.Grey)
                {
                    //Select all cards in hand that are not in our desired color AND are not locomotives.
                    var matchingCard = Hand.Where(x => x.Color != TrainColor.Locomotive
                                                       && !DesiredColors.Contains(x.Color))
                                           .GroupBy(x => x)
                                           .Select(group => new
                                           {
                                               Metric = group,
                                               Count = group.Count()
                                           })
                                           .OrderByDescending(x => x.Count)
                                           .Select(x => x.Metric.Key)
                                           .FirstOrDefault();

                    if (matchingCard == null) continue;

                    selectedColor = matchingCard.Color;

                }

                //Now attempt to claim the specified route with the selected color.
                return ClaimRoute(route, selectedColor);
            }

            return false;
        }

        public bool ClaimRoute(BoardRoute route, TrainColor color)
        {
            //If we don't have enough train cars remaining to claim this route, we cannot do so.
            if (route.Length > RemainingTrainCars)
                return false;

            //First, see if we have enough cards in the hand to claim this route.
            var colorCards = Hand.Where(x => x.Color == color).ToList();

            //If we don't have enough color cards for this route...
            if(colorCards.Count < route.Length)
            {
                //...see if we have enough Locomotive cards to fill the gap
                var gap = route.Length - colorCards.Count;
                var locomotiveCards = Hand.Where(x => x.Color == TrainColor.Locomotive).ToList();
                if(locomotiveCards.Count < gap)
                {
                    return false; //Cannot claim this route.
                }

                var matchingWilds = Hand.GetMatching(TrainColor.Locomotive, gap);
                Board.DiscardPile.AddRange(matchingWilds);

                if (matchingWilds.Count != route.Length)
                {
                    var matchingColors = Hand.GetMatching(colorCards.First().Color, colorCards.Count);
                    Board.DiscardPile.AddRange(matchingColors);
                }

                
                Board.Routes.ClaimRoute(route, this.Color);

                //Add the cities to the list of connected cities
                ConnectedCities.Add(route.Origin);
                ConnectedCities.Add(route.Destination);

                ConnectedCities = ConnectedCities.Distinct().ToList();

                RemainingTrainCars = RemainingTrainCars - route.Length;

                Console.WriteLine(Name + " claims the route " + route.Origin + " to " + route.Destination + " (" + color + ")!");

                return true;
            }

            //If we only need color cards to claim this route, discard the appropriate number of them
            var neededColorCards = Hand.Where(x => x.Color == color).Take(route.Length).ToList();
            foreach(var colorCard in neededColorCards)
            {
                Hand.Remove(colorCard);
                Board.DiscardPile.Add(colorCard);
            }

            //Mark the route as claimed on the board
            Board.Routes.ClaimRoute(route, this.Color);

            RemainingTrainCars = RemainingTrainCars - route.Length;

            Console.WriteLine(Name + " claims the route " + route.Origin + " to " + route.Destination + " (" + color + ")!");

            return true;
        }

        public void ClaimLongestUnclaimedRoute()
        {
            //Find the color we have the most of, so long as it isn't one of the desired colors.
            var mostPopularColor = Hand.GetMostPopularColor(DesiredColors);

            //Now, find a route for that color
            var matchingRoute = Board.Routes.Routes.Where(x => !x.IsOccupied
                                                               && (x.Color == mostPopularColor || x.Color == TrainColor.Grey))
                                                   .OrderByDescending(x => x.Length)
                                                   .FirstOrDefault();

            if(matchingRoute != null)
            {
                ClaimRoute(matchingRoute, mostPopularColor);
            }
        }

        public void CalculateScore()
        {
            //First, we must get combined score of all claimed routes
            var claimedRouteScore = Board.Routes.Routes.Where(x => x.IsOccupied && x.OccupyingPlayerColor == Color)
                                                       .Sum(x => x.PointValue);

            var routes = Board.Routes.Routes.Where(x => x.IsOccupied && x.OccupyingPlayerColor == Color).ToList();

            foreach(var route in routes)
            {
                Console.WriteLine(Name + " claimed the route " + route.Origin + " to " + route.Destination + ", worth " + route.PointValue + " points!");
            }

            //Now, for each destination card, we add the point value if the card is complete, and subtract if they are not
            foreach(var card in DestinationCards)
            {
                string output = Name + " has the destination card " + card.Origin + " to " + card.Destination;

                var isConnected = Board.Routes.IsAlreadyConnected(card.Origin, card.Destination, Color);

                if (isConnected)
                {
                    claimedRouteScore += card.PointValue;
                    output += " (SUCCEEDED)";
                }
                else
                {
                    
                    claimedRouteScore -= card.PointValue;
                    output += " (FAILED)";
                }

                Console.WriteLine(output);

                Board.Routes.AlreadyCheckedCities.Clear();
            }

            Console.WriteLine(Name + " scored " + claimedRouteScore.ToString() + " points!");

            if(claimedRouteScore == 0)
            {
                Console.WriteLine("Please congratulate " + Name + " on having a \"perfect\" game!");
            }
        }
    }
}
