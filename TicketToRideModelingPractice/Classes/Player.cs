using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketToRideModelingPractice.Enums;

namespace TicketToRideModelingPractice.Classes
{
    public class Player
    {
        public string Name { get; set; }

        public List<DestinationCard> DestinationCards { get; set; }

        public List<DestinationCard> CompletedDestinationCards { get; set; }

        public List<BoardRoute> TargetedRoutes { get; set; }

        public PlayerColor Color { get; set; }

        public List<TrainCard> Hand { get; set; }

        public int RemainingTrainCars { get; set; } = 80;

        public List<TrainColor> DesiredColors { get; set; } = new List<TrainColor>();

        public Board Board { get; set; }

        public Player(string name, PlayerColor color, Board board)
        {
            Name = name;
            DestinationCards = board.DestinationCards.Take(3).ToList();
            board.DestinationCards.RemoveAt(0);
            board.DestinationCards.RemoveAt(0);
            board.DestinationCards.RemoveAt(0);
            Color = color;
            Board = board;

            CalculateTargetedRoutes();
        }

        public void CalculateTargetedRoutes()
        {
            var allRoutes = new List<BoardRoute>();
            foreach(var destCard in DestinationCards)
            {
                allRoutes.AddRange(Board.Routes.FindIdealRoute(destCard.Origin, destCard.Destination));
            }

            TargetedRoutes = allRoutes.GroupBy(x => new { x.Origin, x.Destination, x.Color, x.Length })
                                      .Select(group => new
                                      {
                                          Metric = group.Key,
                                          Count = group.Count()
                                      }).OrderByDescending(x => x.Count)
                                        .ThenBy(x => x.Metric.Length)
                                        .Take(5)
                                        .Select(x => new BoardRoute(x.Metric.Origin, x.Metric.Destination, x.Metric.Color, x.Metric.Length))
                                        .ToList();

            DesiredColors = TargetedRoutes.Where(x=>x.Color != TrainColor.Grey)
                                          .Select(x => x.Color)
                                          .Distinct()
                                          .ToList();
        }

        public void OutputPlayerSummary()
        {
            //Player name and color
            Console.WriteLine("Name: " + Name + ", Color: " + Color.ToString());

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

            foreach(var color in DesiredColors)
            {
                Console.WriteLine(color);
            }

            Console.WriteLine("");
        }

        public void TakeTurn()
        {
            //If the player can claim a route they desire, they will do so immediately.

        }
    }
}
