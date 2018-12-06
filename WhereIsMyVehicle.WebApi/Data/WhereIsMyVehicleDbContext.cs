using Microsoft.EntityFrameworkCore;

namespace WhereIsMyVehicle.WebApi.Models
{
    public class WhereIsMyVehicleDbContext : DbContext
    {
        public WhereIsMyVehicleDbContext (DbContextOptions<WhereIsMyVehicleDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Sighting> Sightings { get; set; }
    }
}
