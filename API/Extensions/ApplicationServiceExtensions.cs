
using API.Interfaces;
using API.Services;
using API.Data;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    //extension class must be static
    //must specify 'this' in params when extending a method
    public static class ApplicationServiceExtensions
    {
        //this is what I am extending, app extensions
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //this service is for creating web tokens
            //add scoped uses the service once and then disposes after the http request
            //see interfaces > ITokenService.cs for implementation
            services.AddScoped<ITokenService, TokenService>();

            //insert the DATABASE CLASS in the <>
            //lambda expression
            services.AddDbContext<DataContext>(options => 
            {
                //database connection string
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}