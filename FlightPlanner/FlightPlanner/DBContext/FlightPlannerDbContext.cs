using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.DBContext
{
    public class FlightPlannerDbContext : DbContext
    {
        public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Flight> FLights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
