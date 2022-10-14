using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Business.Models;
using ProjectAPI.Data.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Business.Services.RoleService
{
    public interface IRoleService
    {
        Task<Role> AddRole(RoleModel roleModel);
        Task<ResponseModel> UpdateRole(int roleId, UpdateRoleModel updateRoleModel);
        Task<ResponseModel> DeleteRole(int roleId);   
        Task<IEnumerable<Role>> GetRole();
        Task<Role> GetRoleById(int roleId);

    }
}
