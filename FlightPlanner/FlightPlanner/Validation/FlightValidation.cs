using System;
using System.Collections.Generic;
using FlightPlanner.Models;

namespace FlightPlanner.Validation
{
    public class FlightValidation
    {
        public static bool IsValid(Flight flight)
        {
            if (flight?.To == null)
                return false;

            if (flight?.From == null)
                return false;

            if (string.IsNullOrEmpty(flight.To.AirportCode) ||
                string.IsNullOrEmpty(flight.To.City) ||
                string.IsNullOrEmpty(flight.To.Country))
                return false;

            if (string.IsNullOrEmpty(flight.From.AirportCode) ||
                string.IsNullOrEmpty(flight.From.City) ||
                string.IsNullOrEmpty(flight.From.Country))
                return false;

            if (string.IsNullOrEmpty(flight.ArrivalTime) ||
                string.IsNullOrEmpty(flight.DepartureTime) ||
                string.IsNullOrEmpty(flight.Carrier))
                return false;

            if (flight.From.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower())
            {
                return false;
            }

            if (DateTime.Parse(flight.ArrivalTime) <= DateTime.Parse(flight.DepartureTime))
            {
                return false;
            }

            return true;
        }
    }
}
