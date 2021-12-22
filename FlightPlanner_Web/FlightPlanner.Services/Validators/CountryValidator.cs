using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators
{
    public class CountryValidator: IValidator
    {
        public bool Validate(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request?.To?.Country) 
                   && !string.IsNullOrEmpty(request?.From?.Country);
        }
    }
}
