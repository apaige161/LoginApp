
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{

    //DTO is data transfer object
    public class RegisterDto
    {
        //validate username and password by using data annotations
        //just having the required attribute will not let a null value go through
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}