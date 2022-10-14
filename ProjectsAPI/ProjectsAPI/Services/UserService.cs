using ProjectAPI.Data.EFModels;
using ProjectAPI.Data.Models;
using ProjectsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectAPI.Models;
using ProjectAPI.Business.Models;
using Microsoft.AspNetCore.Identity;

namespace ProjectsAPI.Services
{
    public interface IUserService
    {
        Task<Registration> Register(RegisterModel registerModel);
        Task<ResponseModel> UpdateUser(int userId, UserDto userDto);
        Task<ResponseModel> DeleteUser(int userId);
        Task<IEnumerable<Registration>> GetUser();
        Task<Registration> GetUser(int UserId);
    }

    public class UserService : IUserService
    {
        private readonly UserDbContext dbContext;

        public UserService(UserDbContext _dbContext, IConfiguration configuration)
        {
            this.dbContext = _dbContext;
            configuration = configuration;
        }

        public async Task<Registration> Register(RegisterModel registerModel)
        {
            Registration registration = new Registration();

            try
            {
                registration.FirstName = registerModel.FirstName;
                registration.LastName = registerModel.LastName;
                registration.Address = registerModel.Address;
                registration.Role = registerModel.Role;
                registration.Status = true;
                registration.Password = registerModel.Password;

                await dbContext.SaveChangesAsync();
                await dbContext.Registration.AddAsync(registration);
                return registration;
            }
            catch (Exception e)
            {
                return registration;
            }
        }

        public async Task<ResponseModel> UpdateUser(int userId, UserDto userDto)
        {
            Registration user = new Registration();
            try
            {
                var User_data = await dbContext.Registration.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (User_data != null)
                {
                    User_data.FirstName = userDto.FirstName;
                    User_data.LastName = userDto.LastName;
                    User_data.Email = userDto.Email;
                    User_data.Address = userDto.Address;
                    var role = await dbContext.Roles.Where(x => x.Roles == userDto.Role).FirstOrDefaultAsync();
                    if (role != null)
                        User_data.Role = role.Roles;
                    else
                        return new ResponseModel { StatusCode = StatusCodes.Status400BadRequest, Message = "Incorrect Role" };
                    dbContext.Entry(User_data).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                    return new ResponseModel { StatusCode = StatusCodes.Status200OK, Message = "Updated successfully" };
                }
                return new ResponseModel { StatusCode = StatusCodes.Status400BadRequest, Message = "No such user found" };
            }
            catch (Exception handler)
            {
                return new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = handler.Message };
            }
        }
        public async Task<ResponseModel> DeleteUser(int userId)
        {
            Registration user = new Registration();
            try
            {
                var userData = await dbContext.Registration.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (userData != null)
                {
                    userData.Status = false;
                    dbContext.Entry(userData).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                    return new ResponseModel { StatusCode = StatusCodes.Status200OK, Message = "Deleted successfully" };
                }
                else
                    return new ResponseModel { StatusCode = StatusCodes.Status400BadRequest, Message = "No such user found" };
            }
            catch (Exception handler)
            {
                return new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = handler.Message };
            }
        }
        public async Task<IEnumerable<Registration>> GetUser()
        {
            var data = await dbContext.Registration.Where(x => x.Status == true).ToListAsync();
            return data;
        }
        public async Task<Registration> GetUser(int UserId)
        {
            var data = await dbContext.Registration.Where(x => x.UserId == UserId).FirstOrDefaultAsync();

            return data;
        }

    }
}
