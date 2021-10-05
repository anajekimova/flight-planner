using FlightPlanner.Models;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.DBContext;
using Microsoft.EntityFrameworkCore;
using PageResult = FlightPlanner.Models.PageResult;

namespace FlightPlanner.Storage
{
    public static class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 1;

        public static Flight GetById(int id, FlightPlannerDbContext context)
        {
            return _flights.SingleOrDefault(f => f.Id == id);
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

        public static bool Exists(Flight flight, FlightPlannerDbContext context)
        {
            return context.FLights
                .Include(a => a.To)
                .Include(a => a.From)
                .Any(f => f.ArrivalTime == flight.ArrivalTime
                          && f.DepartureTime == flight.DepartureTime
                          && f.Carrier == flight.Carrier
                          && f.From.AirportCode == flight.From.AirportCode
                          && f.To.AirportCode == flight.To.AirportCode);
        }

        public static List<Airport> SearchAirportsByCode(string search, FlightPlannerDbContext context)
        {
            search = search.ToUpper().Trim();
            List<Airport> airportsCode = new List<Airport>();

            foreach (Airport flight in context.Airports)
            {
                if (flight.City.ToUpper().Contains(search) ||
                    flight.Country.ToUpper().Contains(search) ||
                    flight.AirportCode.ToUpper().Contains(search))
                    airportsCode.Add(flight);
                return airportsCode;
            }

            return airportsCode;
        }

        public static PageResult SearchFlight(SearchFlightsRequest searchFlightsRequest,FlightPlannerDbContext context)
        {
            var flight = context.FLights
                .Include(a=> a.To)
                .Include(a=> a.From)
                .Where(f => f.From.AirportCode == searchFlightsRequest.From ||
                                             f.To.AirportCode == searchFlightsRequest.To
                                             || f.DepartureTime == searchFlightsRequest.Date).ToList();
            return new PageResult(flight);
        }

    }
}
