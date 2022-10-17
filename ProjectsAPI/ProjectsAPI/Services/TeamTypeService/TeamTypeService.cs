using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjectAPI.Data.Models;
using ProjectAPI.Business.Models;
using ProjectAPI.Data.EFModels;
using ProjectAPI.Models;

namespace ProjectAPI.Services.TeamTypeService
{
    public  class TeamTypeService : ITeamTypeService
    {
        private readonly UserDbContext dbContext;
        public TeamTypeService(UserDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<ResponseModel> AddType(TeamTypeModel teamTypeModel)
        {
            TeamType teamType = new TeamType();
            try
            {
                //var team_data = await dbContext.TeamTypes.Where(x => x.TeamType1 == teamTypeModel.TeamType).FirstOrDefaultAsync();
                if (await dbContext.TeamType.Where(x => x.TeamType1 == teamTypeModel.TeamType).FirstOrDefaultAsync() == null)
                {
                    teamType.Status = true;
                    teamType.TeamType1 = teamTypeModel.TeamType;
                    await dbContext.TeamType.AddAsync(teamType);
                    await dbContext.SaveChangesAsync();
                    return new ResponseModel { StatusCode = StatusCodes.Status200OK, Message = "Success" };
                }
                else
                    return new ResponseModel { StatusCode = StatusCodes.Status400BadRequest, Message = string.Format("{0} is already exist", teamTypeModel.TeamType) };
            }
            catch (Exception e)
            {
                return new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = e.Message };
            }
        }
        public async Task<ResponseModel> UpdateType(int TeamTypeId, TeamTypeModel teamTypeModel)
        {
          TeamType team = new TeamType();
            try
            {
                var team_data = await dbContext.TeamType.Where(x => x.TeamTypeId == TeamTypeId).FirstOrDefaultAsync();
                if (team_data != null)
                {
                    team_data.TeamType1 = teamTypeModel.TeamType;
                    team_data.Status = teamTypeModel.Status;
                    dbContext.Entry(team_data).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                    return new ResponseModel { StatusCode = StatusCodes.Status200OK, Message = "Updated successfully" };
                }
                else
                    return new ResponseModel { StatusCode = StatusCodes.Status400BadRequest, Message = "Team not exist" };

            }
            catch (Exception Handler)
            {
                return new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = Handler.Message };

            }
        }
        public async Task<ResponseModel> DeleteType(int TeamTypeId)
        {
            TeamType teamType = new TeamType();
            try
            {
                var team_data = await dbContext.TeamType.Where(x => x.TeamTypeId == TeamTypeId).FirstOrDefaultAsync();
                if (team_data != null)
                {
                    team_data.Status = false;
                    dbContext.Entry(team_data).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                    return new ResponseModel { StatusCode = StatusCodes.Status200OK, Message = "Deleted successfully" };
                }
                else
                    return new ResponseModel { StatusCode = StatusCodes.Status400BadRequest, Message = "Team not exist" };
            }
            catch (Exception handler)
            {
                return new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = handler.Message };
            }
        }
        public async Task<IEnumerable<TeamType>> GetTeamType()
        {
            var data = await dbContext.TeamType.Where(x => x.Status == true).ToListAsync();
            return data;
        }
        public async Task<TeamType> GetTeamType(int teamId)
        {
            var data = await dbContext.TeamType.Where(x => x.TeamTypeId == teamId).FirstOrDefaultAsync();
            return data;
        }

    }
}
