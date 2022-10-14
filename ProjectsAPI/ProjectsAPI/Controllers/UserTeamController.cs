using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Business.Models;
using ProjectAPI.Data.Models;
using ProjectAPI.Models;
using ProjectAPI.Services.UserTeamServices;
using System.Net;
using System.Security.Claims;


namespace ProjectAPI.Controllers
{
    [ApiController]
    [Route("UserTeam")]
    public class UserTeamController : Controller
    {
        public readonly IUserTeamService UserTeamService;
        public readonly UserDbContext dbContext;
        public UserTeamController(IUserTeamService _userTeamService,  UserDbContext _dbContext)
        {
            this.UserTeamService = _userTeamService;
            this.dbContext = _dbContext;
        }
        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> AddUserTeam(UserTeamModel userTeamModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var addUserTeam = await UserTeamService.AddUserTeam(userTeamModel);
                    if (addUserTeam != null)
                    {
                        var role = await dbContext.Roles.Where(x => x.Roles == Enumeration.Admin.ToString()).FirstOrDefaultAsync();
                        if (role == null)
                            return BadRequest("Admin role not found");
                        var loginUser = User.FindFirstValue(ClaimTypes.Name);
                        var userRole = await dbContext.Registration.Where(x => x.Role == role.RoleId).FirstOrDefaultAsync();
                        if (userRole != null)
                            return Ok(addUserTeam);
                        else
                            return BadRequest();
                    }
                    else
                        return BadRequest();
                }
                else
                    return BadRequest();

            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = e.Message });
            }
        }
    }
}
