namespace FlightPlanner.Core.Models
{
    public class Airport : Entity
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AirportCode { get; set; }
    }
}
