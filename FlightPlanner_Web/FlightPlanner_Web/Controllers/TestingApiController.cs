using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Web.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        private readonly IDbServiceExtended _service;

        public TestingApiController(IDbServiceExtended service)
        {
            _service = service;
        }

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            _service.DeleteAll<Airport>();
            _service.DeleteAll<Flight>();
            
            
            return Ok();
        }
    }
}
