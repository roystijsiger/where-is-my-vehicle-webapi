using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WhereIsMyVehicle.WebApi.Helpers;
using WhereIsMyVehicle.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using WhereIsMyVehicle.WebApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Routing;
using WhereIsMyVehicle.WebApi.Data;
using WhereIsMyVehicle.WebApi.Extensions.Configuration;

namespace WhereIsMyVehicle.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // configure strongly typed settings object
            var settingConfigurationSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(settingConfigurationSection);
            
            var appSettings = settingConfigurationSection.Get<AppSettings>();

            // Configure basic routing for project
            services.ConfigureMvc(appSettings);

            // Configure an in memory database
            services.ConfigureDatabase(appSettings, _configuration);

            // Configure app authentication
            services.ConfigureAuthentication(appSettings);

            // Configure generation of swagger.json
            services.ConfigureSwagger(appSettings);
        }

        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var appSettings = _configuration.GetSection("AppSettings").Get<AppSettings>();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureAuthentication(appSettings);
            app.UseMvc();

            var redirectOptions = new RewriteOptions();

            // Redirect homepage to github of android app
            redirectOptions.AddRedirect("^$", "https://github.com/roystijsiger/where-is-my-vehicle-android");
            
            app.UseRewriter(redirectOptions);

            app.ConfigureSwagger(appSettings);
        }
    }
}
