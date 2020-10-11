
using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;

using API.Interfaces;
using API.Entities;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

namespace API.Services
{
    public class TokenService : ITokenService   //inherits from IToken
    {
        private readonly SymmetricSecurityKey _key;

        //constructor for token service
        public TokenService(IConfiguration config)
        {
            //create the key
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            //create claim of User using username and id
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //create token metadata
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            //handler
            var tokenHandler = new JwtSecurityTokenHandler();

            //creates token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}