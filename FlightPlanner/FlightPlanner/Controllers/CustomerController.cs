using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Models;
using FlightPlanner.Storage;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [Route("airports")]
        [HttpGet]

        public IActionResult FindAirport(string search)
        {
            var airport = FlightStorage.SearchAirportsByCode(search);
            if (airport.Count > 0)
                return Ok(airport);
            return Ok();
        }

        [Route("flights/search")]
        [HttpPost]

        public IActionResult SearchFlight(SearchFlightsRequest searchFlightsRequest)
        {
            if (searchFlightsRequest.From == searchFlightsRequest.To)
                return BadRequest();
            var flight = FlightStorage.SearchFlight(searchFlightsRequest);
            return Ok(flight);
        }

        [Route("flights/{id}")]
        [HttpGet]

        public IActionResult GetFlight (int id)
        {
            var flight = FlightStorage.GetById(id);
            if (flight == null)
                return NotFound();
            return Ok(flight);
        }
    }
}
