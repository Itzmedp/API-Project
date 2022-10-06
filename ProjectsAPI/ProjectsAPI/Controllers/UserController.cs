using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectsAPI.Models;
using ProjectsAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectsAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public readonly IUserService userService;
        public readonly UserManager<IdentityUser> userManager;
        public readonly IMapper mapper;
    

        public UserController(IConfiguration configuration, IUserService UserService,IMapper _mapper, UserManager<IdentityUser> userManager)
        {
            this.configuration = configuration;
            mapper = _mapper;
            this.userManager = userManager;
            userService = UserService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                UserModel userModel = new UserModel();
                userModel.UserName = registerModel.FirstName + "" + registerModel.LastName;
                userModel.Email = registerModel.Email;

                var result = await userManager.CreateAsync(userModel, registerModel.Password);
                if (result.Succeeded)
                {
                    await userService.Register(registerModel);
                    return Ok("Register Sucessfully");
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var findUser = await userManager.FindByNameAsync(loginModel.UserName);



                if (findUser == null)
                {
                    return BadRequest(new { status = "Error", Message = "UserNotExist" });
                }

                else if (await userManager.CheckPasswordAsync(findUser, loginModel.Password))
                {
                    var userRoles = await userManager.GetRolesAsync(findUser);
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, findUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
                    var token = new JwtSecurityToken(
                        issuer: configuration["AppSettings:ValidIssuer"],
                        audience: configuration["AppSettings:ValidAudience"],
                        expires: DateTime.Now.AddMinutes(10),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );



                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                else
                {
                    return BadRequest(new { status = "Error", Message = "InvalidUser" });
                }
            }
            else
                return BadRequest(ModelState);

        }
    }
}
