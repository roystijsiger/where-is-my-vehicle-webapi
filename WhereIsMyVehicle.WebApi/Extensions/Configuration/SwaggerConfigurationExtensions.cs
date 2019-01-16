using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WhereIsMyVehicle.WebApi.Helpers;

namespace WhereIsMyVehicle.WebApi.Extensions.Configuration
{
    public static class SwaggerConfigurationExtensions
    {
        internal static IServiceCollection ConfigureSwagger(this IServiceCollection services, AppSettings settings)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(settings.Version, new Info { Title = settings.Name, Version = settings.Version });
            });

            return services;
        }

        internal static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, AppSettings settings)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{settings.Version}/swagger.json", settings.Name);
            });

            return app;
        }
    }
}
