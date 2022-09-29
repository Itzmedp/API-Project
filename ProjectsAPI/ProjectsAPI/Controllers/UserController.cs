using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectsAPI.Models;
using ProjectsAPI.Services;

namespace ProjectsAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public readonly IUserService userService;
        public readonly UserManager<IdentityUser> userManager;
        public readonly IMapper mapper;

        public UserController(IConfiguration configuration, IUserService UserService,IMapper _mapper, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            this.mapper = _mapper;
            this.userManager = userManager;
            this.userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }
                UserModel userModel = new UserModel();
                userModel.UserName = registerModel.FirstName + "" + registerModel.LastName;


                var result = await userManager.CreateAsync(userModel, registerModel.Password);
                if (result.Succeeded)
                {
                    await userService.Register(registerModel);
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
