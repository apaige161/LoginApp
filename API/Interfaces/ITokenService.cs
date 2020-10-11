
using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
        //json web tokens
        //will recieve a user and return a json web token to the account controller
        
        string CreateToken(AppUser user);
    }
}