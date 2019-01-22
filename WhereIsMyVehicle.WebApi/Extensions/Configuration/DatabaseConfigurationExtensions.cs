using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhereIsMyVehicle.WebApi.Data;
using WhereIsMyVehicle.WebApi.Helpers;

namespace WhereIsMyVehicle.WebApi.Extensions.Configuration
{
    internal static class DatabaseConfigurationExtensions
    {
        internal static IServiceCollection ConfigureDatabase(this IServiceCollection services, AppSettings settings,
            IConfiguration configuration)
        {
  

            services.AddDbContext<WhereIsMyVehicleDbContext>(c =>
            {
                c.UseSqlServer(configuration.GetConnectionString("WhereIsMyVehicleConnectionString"));
            });

            return services;
        }

    }
}
