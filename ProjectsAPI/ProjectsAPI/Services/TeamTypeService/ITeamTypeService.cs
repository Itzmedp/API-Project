using ProjectAPI.Business.Models;
using ProjectAPI.Data.EFModels;
using ProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Services.TeamTypeService
{
    public interface ITeamTypeService
    {
        Task<ResponseModel> AddType(TeamTypeModel teamTypeModel);
        Task<ResponseModel> UpdateType(int TeamTypeId, TeamTypeModel teamTypeModel);
        Task<ResponseModel> DeleteType(int TeamTypeId);
        Task<IEnumerable<TeamType>> GetTeamType();
        Task<TeamType> GetTeamType(int teamId);
    }
}
