using System;
using System.Linq;
using TicketToRideModelingPractice.Classes;
using TicketToRideModelingPractice.Enums;
using TicketToRideModelingPractice.Extensions;

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

            for(int i = 0; i<4; i++)
            {
                var cards = board.Deck.Pop(4);
                player1.Hand.Add(cards[0]);
                player2.Hand.Add(cards[1]);
                player3.Hand.Add(cards[2]);
                player4.Hand.Add(cards[3]);
            }

            board.PopulateShownCards();

            player1.OutputPlayerSummary();
            player2.OutputPlayerSummary();
            player3.OutputPlayerSummary();
            player4.OutputPlayerSummary();

            int remainingTurns = -1;
            bool inFinalTurn = false;

            while(remainingTurns == -1 || remainingTurns > 0)
            {
                if (remainingTurns > 0) remainingTurns--;
                else if (remainingTurns == 0) break;

                player1.TakeTurn();

                if (!inFinalTurn && player1.RemainingTrainCars <= 2)
                {
                    inFinalTurn = true;
                    remainingTurns = 3;
                    
                    Console.WriteLine("FINAL TURN");
                }

                if (remainingTurns > 0) remainingTurns--;
                else if (remainingTurns == 0) break;

                player2.TakeTurn();

                if (!inFinalTurn && player2.RemainingTrainCars <= 2)
                {
                    inFinalTurn = true;
                    remainingTurns = 3;
                    Console.WriteLine("FINAL TURN");
                }

                if (remainingTurns > 0) remainingTurns--;
                else if (remainingTurns == 0) break;

                player3.TakeTurn();

                if (!inFinalTurn && player3.RemainingTrainCars <= 2)
                {
                    inFinalTurn = true;
                    remainingTurns = 3;
                    Console.WriteLine("FINAL TURN");
                }

                if (remainingTurns > 0) remainingTurns--;
                else if (remainingTurns == 0) break;

                player4.TakeTurn();

                if (!inFinalTurn && player4.RemainingTrainCars <= 2)
                {
                    inFinalTurn = true;
                    remainingTurns = 3;
                    Console.WriteLine("FINAL TURN");
                }

                Console.WriteLine(Environment.NewLine);

                //Console.ReadLine();
            }

            Console.WriteLine("GAME OVER!");

            player1.CalculateScore();
            player2.CalculateScore();
            player3.CalculateScore();
            player4.CalculateScore();

            Console.ReadLine();
        }
    }
}
