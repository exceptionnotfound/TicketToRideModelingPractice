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

        public BoardRoute GetDirectRouteForPlayer(City origin, City destination, PlayerColor color)
        {
            var route = Routes.Where(x => (x.Origin == origin && x.Destination == destination && x.IsOccupied && x.OccupyingPlayerColor == color)
                                          || (x.Origin == destination && x.Destination == origin && x.IsOccupied && x.OccupyingPlayerColor == color))
                              .FirstOrDefault();

            return route;
        }

        public BoardRoute GetSpecificRoute(City origin, City destination, TrainColor trainColor, int length)
        {
            var route = GetDirectRoute(origin, destination);

            if (route != null && route.Length == length && route.Color == trainColor) return route;

            return null;
        }

        public List<City> AlreadyCheckedCities = new List<City>();

        public List<BoardRoute> FindIdealUnclaimedRoute(City origin, City destination)
        {
            List<BoardRoute> returnRoutes = new List<BoardRoute>();
           
            if (origin == destination)
            {
                return returnRoutes;
            }

            var masterOriginList = GetConnectingCities(origin);
            var masterDestinationList = GetConnectingCities(destination);

            //If these methods return no routes, there are no possible routes to finish. 
            //So, we return an empty list
            if(!masterOriginList.Any() || !masterDestinationList.Any())
            {
                return new List<BoardRoute>();
            }

            var originCitiesList = masterOriginList.Select(x => x.City);
            var destCitiesList = masterDestinationList.Select(x => x.City);

            bool targetOrigin = true;

            while (!originCitiesList.Intersect(destCitiesList).Any() && originCitiesList.Count() < 500)
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

            //It is possible that there are no connecting routes left.
            //In that case, the collections for master cities get very large
            //If they get very large, assume no connections exist.
            if (originCitiesList.Count() >= 500)
            {
                return new List<BoardRoute>();
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
                returnRoutes.AddRange(FindIdealUnclaimedRoute(origin, midpointCity));
            }

            if (destinationDirectRoute == null)
            {
                returnRoutes.AddRange(FindIdealUnclaimedRoute(midpointCity, destination));
            }

            return returnRoutes;
        }

        public List<CityLength> GetConnectingCities(City origin)
        {
            var destinations = Routes.Where(x => x.Origin == origin && !x.IsOccupied)
                                     .OrderBy(x => x.Length)
                                     .Select(x => new CityLength() { City = x.Destination, Length = x.Length })
                                     .ToList();
            var origins = Routes.Where(x => x.Destination == origin && !x.IsOccupied)
                                .OrderBy(x => x.Length)
                                .Select(x => new CityLength() { City = x.Origin, Length = x.Length })
                                .ToList();

            destinations.AddRange(origins);
            return destinations.Distinct().OrderBy(x => x.Length).ToList();
        }

        public List<CityLength> GetConnectingCitiesForPlayer(City origin, PlayerColor color)
        {
            var destinations = Routes.Where(x => x.Origin == origin && x.IsOccupied && x.OccupyingPlayerColor == color)
                                     .Select(x => new CityLength() { City = x.Destination, Length = x.Length })
                                     .ToList();
            var origins = Routes.Where(x => x.Destination == origin && x.IsOccupied && x.OccupyingPlayerColor == color)
                                .Select(x => new CityLength() { City = x.Origin, Length = x.Length })
                                .ToList();

            destinations.AddRange(origins);
            return destinations.Distinct().OrderBy(x => x.Length).ToList();
        }

        public void ClaimRoute(BoardRoute route, PlayerColor color)
        {
            var matchingRoute = Routes.Where(x => x.Origin == route.Origin
                                              && x.Destination == route.Destination
                                              && x.Color == route.Color
                                              && x.Length == route.Length
                                              && x.IsOccupied == false);

            if(matchingRoute.Any())
            {
                var firstRoute = matchingRoute.First();
                Routes.Remove(firstRoute);
                firstRoute.IsOccupied = true;
                firstRoute.OccupyingPlayerColor = color;
                Routes.Add(firstRoute);

            }
        }

        public bool IsAlreadyConnected(City origin, City destination, PlayerColor color)
        {
            List<BoardRoute> returnRoutes = new List<BoardRoute>();

            if (origin == destination)
            {
                return true;
            }

            var masterOriginList = GetConnectingCitiesForPlayer(origin, color);
            var masterDestinationList = GetConnectingCitiesForPlayer(destination, color);

            //If these methods return no routes, there are no possible routes to finish. 
            //So, we return an empty list
            if (!masterOriginList.Any() || !masterDestinationList.Any())
            {
                return false;
            }

            var originCitiesList = masterOriginList.Select(x => x.City);
            var destCitiesList = masterDestinationList.Select(x => x.City);

            bool targetOrigin = true;

            while (!originCitiesList.Intersect(destCitiesList).Any() && originCitiesList.Count() < 500)
            {
                if (targetOrigin == true)
                {
                    var copyMaster = new List<City>(originCitiesList);
                    foreach (var originCity in copyMaster)
                    {
                        masterOriginList.AddRange(GetConnectingCitiesForPlayer(originCity, color));
                    }
                }
                else
                {
                    var copyMaster = new List<City>(destCitiesList);
                    foreach (var destCity in copyMaster)
                    {
                        masterDestinationList.AddRange(GetConnectingCitiesForPlayer(destCity, color));
                    }
                }
                targetOrigin = !targetOrigin;
            }

            //It is possible that there are no connecting routes left.
            //In that case, the collections for master cities get very large
            //If they get very large, assume no connections exist.
            if (originCitiesList.Count() >= 500)
            {
                return false;
            }

            var midpointCity = originCitiesList.Intersect(destCitiesList).First();

            var originDirectRoute = GetDirectRouteForPlayer(origin, midpointCity, color);
            var destinationDirectRoute = GetDirectRouteForPlayer(midpointCity, destination, color);

            if (originDirectRoute != null)
            {
                returnRoutes.Add(originDirectRoute);
            }

            if (destinationDirectRoute != null)
            {
                returnRoutes.Add(destinationDirectRoute);
            }

            if (originDirectRoute == null)
            {
                return false || IsAlreadyConnected(origin, midpointCity, color);
            }

            if (destinationDirectRoute == null)
            {
                return false || IsAlreadyConnected(midpointCity, destination, color);
            }

            return returnRoutes.Any();
        }

        //public bool IsAlreadyConnected(City origin, City destination, PlayerColor color)
        //{
        //    if (origin == destination)
        //    {
        //        return true;
        //    }

        //    //From the origin, select all routes that are claimed by this player
        //    var claimedOriginRoutes = Routes.Where(x => x.IsOccupied && x.OccupyingPlayerColor == color
        //                                          && (x.Origin == origin || x.Destination == origin)).ToList();

        //    if (!claimedOriginRoutes.Any())
        //        return false;

        //    bool isConnected = false;

        //    //For each of these routes, connect to their destination cities.
        //    var oppositeCities = new List<City>();
        //    foreach(var route in claimedOriginRoutes)
        //    {
        //        //If any of the opposite cities are the sought destination, we are done.
        //        var oppositeCity = route.GetOppositeCity(origin);
        //        if(oppositeCity == destination || oppositeCity == origin)
        //        {
        //            return true;
        //        }

        //        //If not, the new origin is the opposite city, and we are still looking for the original destination
        //        AlreadyCheckedCities.Add(origin);
        //        if (!AlreadyCheckedCities.Contains(oppositeCity))
        //        {
        //            isConnected = isConnected || IsAlreadyConnected(oppositeCity, destination, color);
        //        }
        //    }

        //    return false;
        //}
    }
}
