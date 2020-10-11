using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;




namespace API.Controllers
{
    //the attributes are added in a base controller so they do not need to be added when inheriting from this class

    //attributes
    [ApiController]
    //all routes will start will api
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}