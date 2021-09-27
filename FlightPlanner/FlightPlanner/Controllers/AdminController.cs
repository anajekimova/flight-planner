using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using FlightPlanner.Validation;
using Microsoft.AspNetCore.Authorization;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public static object locker = new object();

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            lock (locker)
            {
                var flight = FlightStorage.GetById(id);
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

                if (FlightStorage.Exists(flight))
                    return Conflict();

                FlightStorage.AddFlight(flight);
                return Created("", flight);
            }
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            lock (locker)
            {
                FlightStorage.DeletFlight(id);
                return Ok();
            }
        }

    }
}
