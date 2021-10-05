using System.Linq;
using FlightPlanner.DBContext;
using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using FlightPlanner.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]

    public class AdminController : ControllerBase
    {
        public static object locker = new object();
        private readonly FlightPlannerDbContext _context;

        public AdminController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            lock (locker)
            {
                var flight = _context.FLights
                    .Include(f => f.To)
                    .Include(f => f.From)
                    .SingleOrDefault(f => f.Id == id);

               if (flight == null)
                    return NotFound();

               return Ok(flight);
            }
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flight flight)
        {
            lock (locker)
            {
                if (!FlightValidation.IsValid(flight))
                    return BadRequest();

                if (FlightStorage.Exists(flight, _context))
                    return Conflict();

                _context.Add(flight);
                _context.SaveChanges();

                return Created("", flight);
            }
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            lock (locker)
            {
                var flightDelete = _context.FLights
                    .Include(a => a.To)
                    .Include(a => a.From)
                    .SingleOrDefault(f => f.Id == id);

                if (flightDelete != null)
                {
                    _context.Airports.Remove(flightDelete.To);
                    _context.Airports.Remove(flightDelete.From);
                    _context.FLights.Remove(flightDelete);
                    _context.SaveChanges();
                }
                return Ok();
            }
        }

    }
}
