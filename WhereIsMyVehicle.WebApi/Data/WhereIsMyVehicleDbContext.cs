using Microsoft.EntityFrameworkCore;
using WhereIsMyVehicle.WebApi.Models;

namespace WhereIsMyVehicle.WebApi.Data
{
    public class WhereIsMyVehicleDbContext : DbContext
    {
        public WhereIsMyVehicleDbContext (DbContextOptions<WhereIsMyVehicleDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Sighting> Sightings { get; set; }
    }
}
