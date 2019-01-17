using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WhereIsMyVehicle.WebApi.Helpers;
using WhereIsMyVehicle.WebApi.Services;

namespace WhereIsMyVehicle.WebApi.Extensions.Configuration
{
    internal static class AuthenticationConfigurationExtensions
    {
        internal static IServiceCollection ConfigureAuthentication(this IServiceCollection services, AppSettings settings)
        {
            var key = Encoding.ASCII.GetBytes(settings.Secret);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(c =>
                {
                    c.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            services.AddScoped<IUserService, UserService>();

            return services;
        }

        internal static IApplicationBuilder ConfigureAuthentication(this IApplicationBuilder app, AppSettings settings)
        {
            app.UseAuthentication();

            return app;
        }
    }
}
