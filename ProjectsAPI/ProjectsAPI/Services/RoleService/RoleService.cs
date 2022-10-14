using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectAPI.Business.Models;
using ProjectAPI.Data.EFModels;
using ProjectAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Business.Services.RoleService
{

    public class RoleService : IRoleService
    {
        private readonly UserDbContext dbContext;
        public RoleService(UserDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        public async Task<Role> AddRole(RoleModel roleModel)
        {
            Role role = new Role();
            try
            {
                var roles = await dbContext.Roles.Where(x => x.Roles == roleModel.Roles).FirstOrDefaultAsync();
                if(roles == null)
                {
                    role.Status = true;
                    role.Roles = roleModel.Roles;
                    await dbContext.Roles.AddAsync(role);
                    await dbContext.SaveChangesAsync();
                    return role;
                }
                else
                return role;


            }
            catch (Exception e)
            {
                return role;

            }
        }
        public async Task<ResponseModel> UpdateRole(int roleId, UpdateRoleModel updateRoleModel)
        {
            Role role = new Role();

            try
            {
                var role_data = await dbContext.Roles.Where(x => x.RoleId == roleId).FirstOrDefaultAsync();
                if (role_data != null)
                {
                    role_data.Roles = updateRoleModel.Roles;
                    role_data.Status = updateRoleModel.Status;
                    dbContext.Entry(role_data).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                    return new ResponseModel { StatusCode = StatusCodes.Status200OK, Message = "Updated successfully" };
                }
                else
                    return new ResponseModel { StatusCode = StatusCodes.Status400BadRequest, Message = "No such role found" };

            }
            catch (Exception Handler)
            {
                return new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = Handler.Message};

            }
        }
        public async Task<IEnumerable<Role>> GetRole()
        {  

            var Roles = await dbContext.Roles.Where(x => x.Status == true).ToListAsync();
            return Roles;
        }
        public async Task<Role> GetRoleById(int roleId)
        {

            var RoleId = await dbContext.Roles.Where(x => x.RoleId == roleId).FirstOrDefaultAsync();
            if (RoleId != null)
            {
                return RoleId;
            }      
            else
                return RoleId;
        }
        public async Task <ResponseModel>DeleteRole(int roleId)
        {
            Registration user = new Registration();
            try
            {
                var deleteRole = await dbContext.Roles.Where(x => x.RoleId == roleId).FirstOrDefaultAsync();
                if(deleteRole != null)
                {
                    deleteRole.Status = false;
                    dbContext.Entry(deleteRole).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                    return new ResponseModel { StatusCode = StatusCodes.Status200OK, Message = "Deleted successfully" };
                }
             else
                return new ResponseModel { StatusCode = StatusCodes.Status400BadRequest, Message = "No such Role found" };

            }
            catch (Exception handler)
            {
                return new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = handler.Message };
            }


        }

    }
}
