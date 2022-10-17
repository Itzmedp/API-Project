using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProjectAPI.Data.EFModels
{
    public partial class Roles
    {
        public Roles()
        {
            Registration = new HashSet<Registration>();
        }

        public int RoleId { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Registration> Registration { get; set; }
    }
}
