using System;
using System.Collections.Generic;
using System.Text;
using TicketToRideModelingPractice.Europe.Enums;

namespace TicketToRideModelingPractice.Europe.Classes
{
    public class BoardRoute
    {
        /// <summary>
        /// The "starting point" of the route.
        /// </summary>
        public City Origin { get; set; }

        /// <summary>
        /// The "ending point" of the route.
        /// </summary>
        public City Destination { get; set; }

        /// <summary>
        /// The color of train cards necessary to claim this route (can be grey)
        /// </summary>
        public TrainColor Color { get; set; }

        /// <summary>
        /// The number of cards necessary to claim this route (1-6)
        /// </summary>
        public int Length { get; set; }

        public bool IsTunnel { get; set; }

        public int LocomotiveCount { get; set; }

        /// <summary>
        /// True if the route is already claimed.
        /// </summary>
        public bool IsOccupied { get; set; }

        /// <summary>
        /// Calculated from the Length value.
        /// </summary>
        public int PointValue
        {
            get
            {
                switch (Length)
                {
                    case 1: return 1;
                    case 2: return 2;
                    case 3: return 4;
                    case 4: return 7;
                    case 5: return 10;
                    case 6: return 15;
                    case 8: return 21;
                    default: return 1;
                }
            }
        }

        /// <summary>
        /// If not null, is the color of the player who has claimed this route.
        /// </summary>
        public PlayerColor? OccupyingPlayerColor { get; set; }

        public BoardRoute(City origin, City destination, TrainColor color, int length)
        {
            Origin = origin;
            Destination = destination;
            Color = color;
            Length = length;
        }

        public BoardRoute(City origin, City destination, TrainColor color, int length, bool isTunnel)
        {
            Origin = origin;
            Destination = destination;
            Color = color;
            Length = length;
            IsTunnel = isTunnel;
        }

        public BoardRoute(City origin, City destination, TrainColor color, int length, int locomotiveCount)
        {
            Origin = origin;
            Destination = destination;
            Color = color;
            Length = length;
            LocomotiveCount = locomotiveCount;
        }
    }
}
