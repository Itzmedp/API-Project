using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Business.Models;
using ProjectAPI.Data.EFModels;
using ProjectAPI.Data.Models;
using ProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Services.UserTeamServices
{
    public interface IUserTeamService
    {
        Task<ResponseModel> AddUserTeam(UserTeamModel userTeamModel);
    }
    public class UserTeamService : IUserTeamService
    {

        private readonly UserDbContext dbContext;
        public UserTeamService(UserDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        public async Task<ResponseModel> AddUserTeam(UserTeamModel userTeamModel)
        {
            UserTeam team = new UserTeam();
            try
            {
                if (await dbContext.UserTeams.Where(x => x.AssignedUser == userTeamModel.AssignedUser).FirstOrDefaultAsync() == null)
                {
                    team.AssignedUser = userTeamModel.AssignedUser;
                    team.Status = true;
                    var userId = await dbContext.Users.Where(x => x.Email == userTeamModel.UserName).FirstOrDefaultAsync();
                    var teamtype = await dbContext.TeamTypes.Where(x => x.TeamType1 == userTeamModel.TeamType).FirstOrDefaultAsync();
                    team.UserId = userId.UserId;
                    team.TeamTypeId = teamtype.TeamTypeId;
                    await dbContext.UserTeams.AddAsync(team);
                    await dbContext.SaveChangesAsync();
                    return new ResponseModel { StatusCode = StatusCodes.Status200OK, Message = "Success" };
                }
                else
                    return new ResponseModel{ StatusCode = StatusCodes.Status400BadRequest, Message = "User already assigned to team" };

            }
            catch (Exception Handler)
            {
                return new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = Handler.Message };

            }
        }
       
       
    }
}

