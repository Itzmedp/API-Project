using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProjectAPI.Data.EFModels
{
    public partial class Registration
    {
        public Registration()
        {
            UserDetail = new HashSet<UserDetail>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        public bool Status { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<UserDetail> UserDetail { get; set; }
    }
}
