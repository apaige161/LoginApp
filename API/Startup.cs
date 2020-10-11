using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//import the data namespace for sqlite access
using API.Data;
using API.Services;
using API.Interfaces;
using API.Extensions;


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;



namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            //using encapsulation, access the above property when we want to use it in our class
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        //dependancy injection container
        public void ConfigureServices(IServiceCollection services)
        {
            //add all services

            //uses the services defined in ApplicationServiceExtnsions.cs inside the extensions directory
            services.AddApplicationServices(_config);
            
            //controller service
            services.AddControllers();

            //cors so that client side can access resources in the api
            services.AddCors();

            //login/register/identity services
            services.AddIdentityServices(_config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //must be between these two, order matters
            //this allows (GET/POST) or any method from the specified place
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

            //authenticate and then authorize
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //checks Controllers to handle end points
                endpoints.MapControllers();
            });
        }
    }
}
