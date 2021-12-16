using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Web.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IEntityService<Flight> _flightService;

        public AdminApiController(IEntityService<Flight> flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            //lock (locker)
            {
                var flights = _flightService.GetById(id);
                if (flights == null)
                    return NotFound();

                return Ok(id);
            }
        }
    }
}
