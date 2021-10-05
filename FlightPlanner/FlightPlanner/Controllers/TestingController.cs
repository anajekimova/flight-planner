using FlightPlanner.DBContext;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Storage;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {

        private readonly FlightPlannerDbContext _context;

        public TestingController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            _context.RemoveRange(_context.FLights);
            _context.RemoveRange(_context.Airports);
            _context.SaveChanges();
            return Ok();
        }
    }
}
