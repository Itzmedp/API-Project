using System;
using System.Collections.Generic;

namespace ProjectAPI.Data.EFModels
{
    public partial class UserDetail
    {
        public int UserDetailId { get; set; }
        public string Experience { get; set; } = null!;
        public DateTime DateOfJoining { get; set; }
        public string PreviousOrganizationName { get; set; } = null!;
        public string CurrentOrganizationName { get; set; } = null!;
        public int UserId { get; set; }

        public virtual Registration Registration { get; set; } = null!;
    }
}
