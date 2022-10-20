using ProjectAPI.Business.Models;
using ProjectAPI.Data.EFModels;
using ProjectAPI.Models;
using ProjectsAPI.Models;

namespace ProjectsAPI.Services
{
    public interface IUserService
    {
        Task<ResponseModel> Register(RegisterModel registerModel, string role);
        Task<ResponseModel> UpdateUser(int userId, UserDto userDto);
        Task<ResponseModel> DeleteUser(int userId);
        Task<IEnumerable<UserResponseModel>> GetUser();
        Task<UserResponseModel> GetUser(int UserId);
    }
}
