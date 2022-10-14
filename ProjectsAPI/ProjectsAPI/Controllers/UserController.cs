using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectAPI.Business.Models;
using ProjectAPI.Data.EFModels;
using ProjectAPI.Models;
using ProjectsAPI.Models;
using ProjectsAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ProjectsAPI.Controllers
{
    [ApiController]
    [EnableCors("NgOrigins")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public readonly IUserService userService;
        public readonly UserManager<IdentityUser> userManager;
        public readonly IMapper mapper;


        public UserController(IConfiguration configuration, IUserService UserService, IMapper _mapper, UserManager<IdentityUser> userManager)
        {
            this.configuration = configuration;
            mapper = _mapper;
            this.userManager = userManager;
            userService = UserService;
        }
        [HttpPost]
        [Route("[controller]/[action]")]
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
        [Route("[controller]/[action]")]
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

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<ActionResult> UpdateUser(int UserId, UserDto userDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(userDto.Email);
                    if (user == null)
                        return BadRequest();
                    else
                    {
                        UserModel userModel = new UserModel();
                        userModel.UserName = userDto.Email;
                        userModel.Email = userDto.Email;
                        var users = await userManager.UpdateAsync(userModel);

                        var updateUser = await userService.UpdateUser(UserId, userDto);
                        if (updateUser != null)
                        {
                            if (updateUser.StatusCode == StatusCodes.Status200OK)
                            {

                                return Ok(updateUser);

                            }
                            else
                                return BadRequest(updateUser);
                        }
                        else
                            return BadRequest(updateUser);
                    }
                }
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = e.Message });
            }
        }

        [HttpDelete]
        [Route("[controller]/[action]")]
        public async Task<ActionResult> DeleteUser(int UserId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var deleteUser = await userService.DeleteUser(UserId);
                    if (deleteUser != null)
                    {
                        if (deleteUser.StatusCode == StatusCodes.Status200OK)
                            return Ok(deleteUser);
                        else
                            return BadRequest(deleteUser);
                    }
                    else
                        return BadRequest(deleteUser);
                }
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<ActionResult<Registration>> GetUser()
        {
            try
            {
                return Ok(await userService.GetUser());
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("[controller]/[action]/id")]
        public async Task<ActionResult<Registration>> GetByIdUser(int UserId)
        {
            var data = await userService.GetUser(UserId);
            return Ok(data);
        }

    }
}
