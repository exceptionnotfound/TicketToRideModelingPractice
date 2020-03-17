using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketToRideModelingPractice.Enums;
using TicketToRideModelingPractice.Extensions;

namespace TicketToRideModelingPractice.Classes
{
    public class BoardRouteCollection
    {
        public List<BoardRoute> Routes { get; set; } = new List<BoardRoute>();

        public void AddRoute(City origin, City destination, TrainColor color, int length)
        {
            Routes.Add(new BoardRoute(origin, destination, color, length));
        }

        public void MarkComplete(BoardRoute route)
        {
            var matchingRoute = Routes.First(x => (x.Origin == route.Origin
                                                       && x.Destination == route.Destination)
                                                  || (x.Origin == route.Destination
                                                       && x.Destination == route.Origin)
                                                  && x.Length == route.Length
                                                  && x.Color == route.Color);
            Routes.Remove(matchingRoute);
            route.IsOccupied = true;
            Routes.Add(route);
        }

        public BoardRoute GetDirectRoute(City origin, City destination)
        {
            var route = Routes.Where(x => (x.Origin == origin && x.Destination == destination && !x.IsOccupied)
                                          || (x.Origin == destination && x.Destination == origin && !x.IsOccupied))
                              .FirstOrDefault();

            return route;
        }

        public BoardRoute GetSpecificRoute(City origin, City destination, TrainColor trainColor, int length)
        {
            var route = GetDirectRoute(origin, destination);

            if (route != null && route.Length == length && route.Color == trainColor) return route;

            return null;
        }

        public List<BoardRoute> FindIdealRoute(City origin, City destination)
        {
            List<BoardRoute> returnRoutes = new List<BoardRoute>();
           
            if (origin == destination)
            {
                return returnRoutes;
            }

            var masterOriginList = GetConnectingCities(origin);
            var masterDestinationList = GetConnectingCities(destination);

            var originCitiesList = masterOriginList.Select(x => x.City);
            var destCitiesList = masterDestinationList.Select(x => x.City);

            bool targetOrigin = true;

            while (!originCitiesList.Intersect(destCitiesList).Any())
            {
                if (targetOrigin == true)
                {
                    var copyMaster = new List<City>(originCitiesList);
                    foreach (var originCity in copyMaster)
                    {
                        masterOriginList.AddRange(GetConnectingCities(originCity));
                    }
                }
                else
                {
                    var copyMaster = new List<City>(destCitiesList);
                    foreach (var destCity in copyMaster)
                    {
                        masterDestinationList.AddRange(GetConnectingCities(destCity));
                    }
                }
                targetOrigin = !targetOrigin;
            }

            var midpointCity = originCitiesList.Intersect(destCitiesList).First();

            var originDirectRoute = GetDirectRoute(origin, midpointCity);
            var destinationDirectRoute = GetDirectRoute(midpointCity, destination);

            if (originDirectRoute != null)
            {
                returnRoutes.Add(originDirectRoute);
            }

            if(destinationDirectRoute != null)
            {
                returnRoutes.Add(destinationDirectRoute);
            }

            if (originDirectRoute == null)
            {
                returnRoutes.AddRange(FindIdealRoute(origin, midpointCity));
            }

            if (destinationDirectRoute == null)
            {
                returnRoutes.AddRange(FindIdealRoute(midpointCity, destination));
            }

            return returnRoutes;
        }

        public List<CityLength> GetConnectingCities(City origin)
        {
            var destinations = Routes.Where(x => x.Origin == origin && !x.IsOccupied).Select(x => new CityLength() { City = x.Destination, Length = x.Length }).ToList();
            var origins = Routes.Where(x => x.Destination == origin && !x.IsOccupied).Select(x => new CityLength() { City = x.Origin, Length = x.Length }).ToList();

            destinations.AddRange(origins);
            return destinations.Distinct().OrderBy(x => x.Length).ToList();
        }
    }
}
