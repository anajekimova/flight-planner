using System.Linq;
using FlightPlanner.DBContext;
using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using FlightPlanner.Validation;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private FlightPlannerDbContext _context;

        public CustomerController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [Route("airports")]
        [HttpGet]

        public IActionResult FindAirport(string search, FlightPlannerDbContext context)
        {
            var airport = FlightStorage.SearchAirportsByCode(search, _context);
            
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
            var flight = FlightStorage.SearchFlight(searchFlightsRequest, _context);
            return Ok(flight);
        }

        [Route("flights/{id}")]
        [HttpGet]

        public IActionResult GetFlight (int id)
        {
            var flight = _context.FLights
                .Include(a=> a.To)
                .Include(a=> a.From)
                .SingleOrDefault(f=> f.Id == id);
            _context.SaveChanges();
            if (flight == null)
                return NotFound();
            return Ok(flight);
        }
    }
}
