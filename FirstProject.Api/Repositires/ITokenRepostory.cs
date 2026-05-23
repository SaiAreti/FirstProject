using Microsoft.AspNetCore.Identity;

namespace FirstProject.Api.Repositires
{
    public interface ITokenRepostory
    {

        string CreateToken(IdentityUser user, List<string> roles); 
        
    }
}
