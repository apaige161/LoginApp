using System.Text;

using API.Interfaces;
using API.Services;
using API.Data;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;



namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        //this is what I am extending, identity extensions
        //return an IServiceCollection
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            //authentication service
            //specify authentication scheme
            //authentication middleware
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateIssuer = false,     //issuer is the API server
                        ValidateAudience = false    //Audience is the angular front end
                    };
                });

                return services;
        }
    }
}