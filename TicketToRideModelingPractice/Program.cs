using System;
using System.Linq;
using TicketToRideModelingPractice.Classes;
using TicketToRideModelingPractice.Enums;

namespace TicketToRideModelingPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();

            var player1 = new Player("Alice", PlayerColor.Red, board);
            var player2 = new Player("Bob", PlayerColor.Blue, board);
            var player3 = new Player("Charlie", PlayerColor.Black, board);
            var player4 = new Player("Danielle", PlayerColor.Yellow, board);

            board.ClaimRoute(City.SaintLouis, City.Pittsburgh, TrainColor.Green, 5, PlayerColor.Red);

            player1.CalculateTargetedRoutes();
            player2.CalculateTargetedRoutes();
            player3.CalculateTargetedRoutes();
            player4.CalculateTargetedRoutes();

            player1.OutputPlayerSummary();
            player2.OutputPlayerSummary();
            player3.OutputPlayerSummary();
            player4.OutputPlayerSummary();

            Console.ReadLine();
        }
    }
}
