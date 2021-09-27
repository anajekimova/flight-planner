using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Storage;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            FlightStorage.ClearFlight();
            return Ok();
        }
    }
}
