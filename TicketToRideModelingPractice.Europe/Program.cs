using System;
using System.Collections.Generic;
using System.Text;
using TicketToRideModelingPractice.Europe.Classes;
using TicketToRideModelingPractice.Europe.Enums;
using TicketToRideModelingPractice.Europe.Extensions;

namespace TicketToRideModelingPractice.Europe
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create and setup the game board
            var board = new Board();

            //Create the players
            var player1 = new Player("Alice", PlayerColor.Red, board);
            var player2 = new Player("Bob", PlayerColor.Blue, board);
            var player3 = new Player("Charlie", PlayerColor.Black, board);
            var player4 = new Player("Danielle", PlayerColor.Yellow, board);

            //For each player, deal them 4 train cards
            for (int i = 0; i < 4; i++)
            {
                var cards = board.Deck.Pop(4);
                player1.Hand.Add(cards[0]);
                player2.Hand.Add(cards[1]);
                player3.Hand.Add(cards[2]);
                player4.Hand.Add(cards[3]);
            }

            //Populate the Shown Cards collection
            board.PopulateShownCards();

            //Now that each player has calculated their desired routes and colors,
            //output that information to the command line.
            player1.OutputPlayerSummary();
            player2.OutputPlayerSummary();
            player3.OutputPlayerSummary();
            player4.OutputPlayerSummary();

            int remainingTurns = -1;
            bool inFinalTurn = false;

            //While we are not in the final turn, have each player take turns.
            while (remainingTurns == -1 || remainingTurns > 0)
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
