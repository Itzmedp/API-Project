using ProjectAPI.Data.EFModels;
using ProjectAPI.Data.Models;
using ProjectsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ProjectsAPI.Services
{
    public interface IUserService
    {
        Task<Registration> Register(RegisterModel registerModel);
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
                registration.Status = registerModel.Status;
                registration.Password = registerModel.Password;

                await dbContext.SaveChangesAsync();
                await dbContext.Registrations.AddAsync(registration);
                return registration;
            }
            catch (Exception e)
            {
                return registration;
            }
        }
    }
}
