
using System;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using API.Entities;
using API.Data;
using API.DTOs;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        //constructor
        //inject IToken service
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        //create and add a new user to the database
        [HttpPost("register")]
        //returns an AppUser
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            //validate username --can return http status codes bc of the ActionResult 
            if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            //provides hashing algor
            //by the 'using' keyword the method will be disposed of when it is finished
            using var hmac = new HMACSHA512();

            //create user
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                //get the hash of paswword
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                //generate salt(key)
                PasswordSalt = hmac.Key
            };

            //tracking this in entity framework
            _context.Users.Add(user);
            //save user into database table
            await _context.SaveChangesAsync();

            //return data transfer object
            //initializes the new object
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        //login endpoint
        [HttpPost("login")]
        //returns AppUser
        public async Task<ActionResult<UserDto>> login(LoginDto loginDto)
        {
            //find the user
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            //if no user is found return error
            if(user == null) return Unauthorized("Invalid username");

            //reverse password salt
            using var hmac = new HMACSHA512(user.PasswordSalt);

            //if the password here is the same password before the salt than the two should be identical
            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            //loop over each element in the byte array
            for(int i = 0; i < ComputeHash.Length; i++)
            {
                
                if(ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            //return data transfer object
            //initializes the new object
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }



        //check to see if user name is already taken
        private async Task<bool> UserExists (string userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName.ToLower());
        }
    }
}