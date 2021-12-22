using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightPlanner.Core.Dto;
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
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidator> _validators;
        public AdminApiController(IFlightService flightService, IMapper mapper, IEnumerable<IValidator> validators)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validators = validators;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            //lock (locker)
            {

                var flights = _flightService.GetFullFlightById(id);
                if (flights == null)
                    return NotFound();

                return Ok(_mapper.Map<FlightResponse>(flights));
            }
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(FlightRequest request)
        {
            // (locker)
            {
                //if (!FlightValidation.IsValid(flight))
                //return BadRequest();
                if (!_validators.Any(s => s.Validate(request)))
                    return BadRequest();
                var flight = _mapper.Map<Flight>(request);

                if (_flightService.Exists(flight))
                 return Conflict();
    

                

                _flightService.Create(flight);

                return Created("", _mapper.Map<FlightResponse>(flight));
            }
        }
    }
}
