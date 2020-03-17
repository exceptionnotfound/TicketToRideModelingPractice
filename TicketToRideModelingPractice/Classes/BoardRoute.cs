using System;
using System.Collections.Generic;
using System.Text;
using TicketToRideModelingPractice.Enums;

namespace TicketToRideModelingPractice.Classes
{
    public class BoardRoute
    {
        public City Origin { get; set; }
        public City Destination { get; set; }
        public TrainColor Color { get; set; }
        public int Length { get; set; }
        public bool IsOccupied { get; set; }
        public int PointValue
        {
            get
            {
                switch(Length)
                {
                    case 1: return 1;
                    case 2: return 2;
                    case 3: return 4;
                    case 4: return 7;
                    case 5: return 10;
                    case 6: return 15;
                    default: return 1;
                }
            }
        }
        public PlayerColor? OccupyingPlayerColor { get; set; }

        public BoardRoute(City origin, City destination, TrainColor color, int length)
        {
            Origin = origin;
            Destination = destination;
            Color = color;
            Length = length;
        }

        public string Display()
        {
            return Origin.ToString() + " -> " + Destination.ToString() + "(" + Length.ToString() + ", " + Color.ToString() + ")";
        }
    }
}
