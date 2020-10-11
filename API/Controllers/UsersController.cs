using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//import local data
using API.Data;
using API.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

//the view will come from angular on the client side
namespace API.Controllers
{
    
    //derive from BaseApiController and ControllerBase
    //a derived class recieves all of the attributes, properties and methods from that parent class
    public class UsersController : BaseApiController
    {
        //generate a constructor
            //then generate a private field

        private readonly DataContext _context;
        //add using API.data for DataContext
        public UsersController(DataContext context)
        {
            _context = context;
        }

        //add user endpoints
            //get all users
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            //get users from a database to a list
            //output the users
            return await _context.Users.ToListAsync();

        }

        //get a single user
        //api/users/3   -user of id 3 to get the one user
        //Authorize--user endpoint is now protected
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            //FIND user by id
            //output the user
            return await _context.Users.FindAsync(id);

        }
    }
}