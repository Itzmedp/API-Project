using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Business;
using ProjectAPI.Business.Models;
using ProjectAPI.Business.Services.RoleService;
using ProjectAPI.Data.EFModels;
using ProjectAPI.Data.Models;
using System.Security.Claims;


namespace ProjectAPI.Controllers
{
    [ApiController]
    public class RoleController : ControllerBase
    {
        public readonly IRoleService RoleService;
        public readonly RoleManager<IdentityRole> roleManager;
        public readonly UserDbContext dbContext;
        public RoleController(IRoleService _roleService, RoleManager<IdentityRole> _roleManager, UserDbContext _dbContext)
        {
            this.RoleService = _roleService;
            this.roleManager = _roleManager;
            this.dbContext = _dbContext;
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<ActionResult> AddRole(RoleModel roleModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = await dbContext.Roles.Where(x => x.Role == Enumeration.Admin.ToString()).FirstOrDefaultAsync();
                    if (role == null)
                        return BadRequest("Admin role not found");
                    var loginUser = User.FindFirstValue(ClaimTypes.Name);
                    var userRole = await dbContext.Registration.Where(x => x.RoleId == role.RoleId).FirstOrDefaultAsync();
                    var roleCheck = await dbContext.Roles.Where(x => x.Role == roleModel.Roles).FirstOrDefaultAsync();
                    if (roleCheck != null)
                    {
                        RolesModel rolesModel = new RolesModel();
                        rolesModel.Name = roleModel.Roles;
                        var createRole = await roleManager.CreateAsync(rolesModel);
                        var addRole = await RoleService.AddRole(roleModel);
                        if (addRole != null && createRole.Succeeded)
                        {
                            return Ok(addRole);
                        }
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

        [HttpPut]
        [Route("[controller]/[action]")]
        public async Task<ActionResult> UpdateRole(int roleId, UpdateRoleModel updateRoleModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role_data = await RoleService.UpdateRole(roleId, updateRoleModel);
                    RolesModel rolesModel = new RolesModel();
                    if (role_data != null)
                    {
                        var role = await dbContext.Roles.Where(x => x.Role == Enumeration.Admin.ToString()).FirstOrDefaultAsync();
                        if (role == null)
                            return BadRequest("Admin role not found");
                        var loginUser = User.FindFirstValue(ClaimTypes.Name);
                        var userRole = await dbContext.Registration.Where(x => x.RoleId == role.RoleId).FirstOrDefaultAsync();
                        rolesModel.Name = updateRoleModel.Roles;
                        var updatRole = await roleManager.UpdateAsync(rolesModel);
                        if (role_data.StatusCode == StatusCodes.Status200OK)
                        {
                            if (updatRole.Succeeded)
                                return Ok(role_data);
                            else
                                return BadRequest(updatRole.Errors);
                        }
                        else
                            return BadRequest(role_data);
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

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<ActionResult<Roles>> GetRole()
        {
            try
            {
                var role = await dbContext.Roles.Where(x => x.Role == Enumeration.Admin.ToString()).FirstOrDefaultAsync();
                if (role == null)
                    return BadRequest("Admin role not found");
                var loginUser = User.FindFirstValue(ClaimTypes.Name);
                var userRole = await dbContext.Registration.Where(x => x.RoleId == role.RoleId).FirstOrDefaultAsync();
                if (userRole != null)
                    return Ok(await RoleService.GetRole());
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = e.Message });
            }
        }
        [HttpGet]
        [Route("[controller]/[action]/id")]
        public async Task<ActionResult> GetByIdUser(int UserId)
        {
            var data = await RoleService.GetRoleById(UserId);
            if (data != null)
            {
                var role = await dbContext.Roles.Where(x => x.Role == Enumeration.Admin.ToString()).FirstOrDefaultAsync();
                if (role == null)
                    return BadRequest("Admin role not found");
                var loginUser = User.FindFirstValue(ClaimTypes.Name);
                var userRole = await dbContext.Registration.Where(x => x.RoleId == role.RoleId).FirstOrDefaultAsync();
                if (userRole != null)
                    return Ok(data);
                else
                    return BadRequest();
            }
            else
                return BadRequest(data);
        }


    }
}
