using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using WhereIsMyVehicle.WebApi.Helpers;

namespace WhereIsMyVehicle.WebApi.Extensions.Configuration
{
    internal static class MvcConfigurationExtensions
    {
        internal static IServiceCollection ConfigureMvc(this IServiceCollection services, AppSettings settings)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<RouteOptions>(options => 
            {
                options.LowercaseUrls = true;
            });

            return services;
        }
    }
}
