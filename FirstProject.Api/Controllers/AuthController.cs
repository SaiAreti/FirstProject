using FirstProject.Api.Model.DTO;
using FirstProject.Api.Repositires;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        public ITokenRepostory tokenRepostory { get; }
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepostory tokenRepostory)
        {
            this.userManager = userManager;
            this.tokenRepostory = tokenRepostory;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterAuthDTO registerAuthDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerAuthDTO.UserName,
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerAuthDTO.Password);
            if (identityResult.Succeeded)
            {
                if (registerAuthDTO.roles != null && registerAuthDTO.roles.Length > 0)
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerAuthDTO.roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User created successfully with roles");
                    }

                }

            }

            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] loginDetailsDTO loginDetails)
        {
            //var user = await userManager.FindByEmailAsync(loginDetails.UserName);
            var user = await userManager.FindByNameAsync(loginDetails.UserName);
            if (user != null)
            {
                var isPasswordValid = await userManager.CheckPasswordAsync(user, loginDetails.Password);
                if (isPasswordValid)
                {
                    //Get role from user manager
                     var roles = await userManager.GetRolesAsync(user);

                    //Create jwtToken 
                    if (roles != null) 
                    {                         
                        var jwtToken = tokenRepostory.CreateToken(user, roles.ToList());

                        var response = new loginResponseTokenDTO
                        {
                            jwtToken = jwtToken
                        };
                        return Ok(response);


                    }
                   // return Ok("Login successful");
                }
            }
            return Unauthorized();

        }
    }
}
