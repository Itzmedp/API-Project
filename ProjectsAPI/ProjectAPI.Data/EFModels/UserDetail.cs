using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProjectAPI.Data.EFModels
{
    public partial class UserDetail
    {
        public int UserDetailId { get; set; }
        public string Experience { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string PreviousOrganizationName { get; set; }
        public string CurrentOrganizationName { get; set; }
        public int UserId { get; set; }

        public virtual Registration User { get; set; }
    }
}
