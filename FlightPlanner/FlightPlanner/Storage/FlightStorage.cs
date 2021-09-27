using FlightPlanner.Models;
using System.Collections.Generic;
using System.Linq;
using PageResult = FlightPlanner.Models.PageResult;

namespace FlightPlanner.Storage
{
    public static class  FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 1;

        public static Flight GetById(int id)
        {
           return  _flights.SingleOrDefault(f => f.Id == id);
        }

        public static void ClearFlight()
        {
            _flights.Clear();
        }

        public static Flight AddFlight(Flight flight)
        {
            flight.Id = _id;
                _id++;
                _flights.Add(flight);
                return flight;
        }

        public static void DeletFlight(int id)
        {
            var flightDelete = _flights.SingleOrDefault(f => f.Id == id);
            if (flightDelete != null)
                _flights.Remove(flightDelete);
        }

        public static bool Exists(Flight flight)
        {
            return _flights.Any(f => f.ArrivalTime == flight.ArrivalTime
                                     && f.DepartureTime == flight.DepartureTime
                                     && f.Carrier == flight.Carrier
                                     && f.From.AirportCode == flight.From.AirportCode
                                     && f.To.AirportCode == flight.To.AirportCode);
        }

        public static List<Airport> SearchAirportsByCode(string search)
        {
            search = search.ToUpper().Trim();
            List<Airport> airportsCode = new List<Airport>();

            foreach (Flight flight in _flights)
            {
                if (flight.From.City.ToUpper().Contains(search) ||
                    flight.From.Country.ToUpper().Contains(search) ||
                    flight.From.AirportCode.ToUpper().Contains(search))
                    airportsCode.Add(flight.From);
                return airportsCode;
            }

            return airportsCode;
        }

        public static PageResult SearchFlight(SearchFlightsRequest searchFlightsRequest)
        {
            var flight = _flights.Where(f => f.From.AirportCode == searchFlightsRequest.From ||
                                             f.To.AirportCode == searchFlightsRequest.To
                                             || f.DepartureTime == searchFlightsRequest.Date).ToList();
            return new PageResult(flight);
        }

    }
}
