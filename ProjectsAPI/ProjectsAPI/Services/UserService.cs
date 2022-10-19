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
    public class UserService : IUserService
    {
        private readonly UserDbContext dbContext;

        public UserService(UserDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<ResponseModel> Register(RegisterModel registerModel, string role)
        {
            Registration registration = new Registration();

            try
            {
                registration.FirstName = registerModel.FirstName;
                registration.LastName = registerModel.LastName;
                registration.Email = registerModel.Email;
                registration.Address = registerModel.Address;
                registration.Status = true;
                var roleID = await dbContext.Roles.Where(x => x.Role == role).FirstOrDefaultAsync();
                if (roleID != null)
                    registration.RoleId = roleID.RoleId;
                else
                    return new ResponseModel { StatusCode = StatusCodes.Status400BadRequest, Message = "Incorrect role" };

                await dbContext.Registration.AddAsync(registration);
                await dbContext.SaveChangesAsync();
                return new ResponseModel { StatusCode = StatusCodes.Status200OK, Message = "Success" };
            }
            catch (Exception Handler)
            {
                return new ResponseModel { StatusCode = StatusCodes.Status500InternalServerError, Message = Handler.Message };
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



        public async Task<ResponseModel> UpdateUser(int userId, UserDto userDto)
        {
            Registration registration = new Registration();
            try
            {
                var User_data = await dbContext.Registration.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (User_data != null)
                {
                    User_data.FirstName = userDto.FirstName;
                    User_data.LastName = userDto.LastName;
                    User_data.Email = userDto.Email;
                    User_data.Address = userDto.Address;
                    var role = await dbContext.Roles.Where(x => x.Role == userDto.Role).FirstOrDefaultAsync();
                    if (role != null)
                        User_data.RoleId = role.RoleId;
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
    }


    }
