using System;
using System.Collections.Generic;

namespace ProjectAPI.Data.EFModels
{
    public partial class Registration
    {   
        public Registration()
        {
            UserTeams = new HashSet<UserTeam>();
        }
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }
        public bool? Status { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<UserTeam> UserTeams { get; set; }
        public string Email { get; set; }
    }
}
