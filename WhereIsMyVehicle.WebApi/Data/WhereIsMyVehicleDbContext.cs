using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhereIsMyVehicle.WebApi.Models;

namespace WhereIsMyVehicle.WebApi.Models
{
    public class WhereIsMyVehicleDbContext : DbContext
    {
        public WhereIsMyVehicleDbContext (DbContextOptions<WhereIsMyVehicleDbContext> options)
            : base(options)
        {
        }

        public DbSet<WhereIsMyVehicle.WebApi.Models.Vehicle> Vehicle { get; set; }

        public DbSet<WhereIsMyVehicle.WebApi.Models.Sighting> Sighting { get; set; }
    }
}
