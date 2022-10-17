using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProjectAPI.Data.EFModels
{
    public partial class UserTeam
    {
        public int UserTeamId { get; set; }
        public int UserDetailId { get; set; }
        public string AssignedUser { get; set; }
        public int TeamTypeId { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }
    }
}
