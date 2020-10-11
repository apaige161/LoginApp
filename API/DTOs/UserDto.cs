namespace API.DTOs
{
    public class UserDto
    {
        //this is the object to return when a user logs in or registers
        public string Username { get; set; }
        public string Token { get; set; }
    }
}