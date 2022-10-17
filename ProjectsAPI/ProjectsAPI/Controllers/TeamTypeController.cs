using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Business;
using ProjectAPI.Business.Models;
using ProjectAPI.Data.Models;
using ProjectAPI.Models;
using ProjectAPI.Services.TeamTypeService;
using System.Security.Claims;

namespace ProjectAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("UserTeam")]
    public class TeamTypeController : ControllerBase
    {
        public readonly ITeamTypeService teamTypeService;
        public readonly UserDbContext dbContext;
        public TeamTypeController(ITeamTypeService teamTypeService, UserDbContext _dbContext)
        {
            this.teamTypeService = teamTypeService;
            this.dbContext = _dbContext;
        }
        [HttpPost]
        [Route("/[controller]/[action]")]
        public async Task<ActionResult> AddTeamType(TeamTypeModel teamTypeModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var addType = await teamTypeService.AddType(teamTypeModel);
                    if (addType != null)
                    {
                        if (addType.StatusCode == StatusCodes.Status200OK)
                        {
                            var role = await dbContext.Roles.Where(x => x.Role == Enumeration.Admin.ToString()).FirstOrDefaultAsync();
                            if (role == null)
                                return BadRequest("Admin role not found");
                            var loginUser = User.FindFirstValue(ClaimTypes.Name);
                            var userRole = await dbContext.Registration.Where(x => x.RoleId == role.RoleId).FirstOrDefaultAsync();
                            if (userRole != null)
                                return Ok(addType);
                            else
                                return BadRequest();
                        }
                        else
                            return BadRequest(addType);
                    }
                    else
                        return BadRequest();                   
                }
                else
                    return BadRequest();

            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel {StatusCode =  StatusCodes.Status500InternalServerError, Message = e.Message});
            }
        }
    }
}
