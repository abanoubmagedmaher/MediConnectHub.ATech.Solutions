using MediConnectHub.DTOS;
using MediConnectHub.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediConnectHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        #region Create New User
        // Create New User "Registration"

        [HttpPost("Registeration")] // POST : api/Account/Registration
        public async Task<IActionResult> Registeration(RegisterUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                //save
                ApplicationUser user = new ApplicationUser();
                user.UserName = userDto.UserName;
                user.Email = userDto.Email;
                IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    return Ok(new {statuscode=200 ,message="Account Added Successfully" });

                }
                BadRequest(result.Errors.FirstOrDefault());

            }
            return ((IActionResult)ModelState);

        }
        #endregion

        #region Login
        // check user Is valid "Login"
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                // check and create Token
                ApplicationUser user = await _userManager.FindByNameAsync(userDto.userName);
                if (user != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, userDto.password);
                    if (found)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        // Get Roles 
                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var item in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item));
                        }

                        // signingCredentials
                        SecurityKey securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                        SigningCredentials signincred = new SigningCredentials(
                            securitykey
                            , SecurityAlgorithms.HmacSha256
                            );

                        // create Token 
                        JwtSecurityToken Mytoken = new JwtSecurityToken(
                            issuer: _config["JWT:ValidIssure"],
                            audience: _config["JWT:Validaudience"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signincred

                            );
                        return Ok(new
                        {
                            user = new
                            {
                                name = user.UserName,
                                email = user.Email,
                                role = "user"
                            },
                            message= "success",
                            // data = user,
                            token = new JwtSecurityTokenHandler().WriteToken(Mytoken),
                            expires = Mytoken.ValidTo
                        });

                    }
                }
                return Unauthorized();
            }
            return Unauthorized();
        } 
        #endregion
    }
}
