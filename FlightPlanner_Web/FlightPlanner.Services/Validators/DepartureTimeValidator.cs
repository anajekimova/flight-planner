﻿using System;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators
{
    public class DepartureTimeValidator : IValidator
    {
        public bool Validate(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request.DepartureTime);
        }
    }
}