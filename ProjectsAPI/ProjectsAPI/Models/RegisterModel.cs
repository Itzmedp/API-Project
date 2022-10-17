using Microsoft.AspNetCore.Identity;
using ProjectAPI.Data.EFModels;

namespace ProjectsAPI.Models
{
    public class RegisterModel 
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public string? Password { get; set; }
        public Roles RoleId { get; internal set; }
    }
}
