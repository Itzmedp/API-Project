using System;
using System.Collections.Generic;

namespace ProjectAPI.Data.EFModels
{
    public partial class UserTeam
    {
        public int UserTeamId { get; set; }
        public int UserId { get; set; }
        public string AssignedUser { get; set; } = null!;
        public string TeamTypeId { get; set; }
        public bool Status { get; set; }

        public virtual TeamType TeamType { get; set; } = null!;
        public virtual Registration User { get; set; } = null!;
    }
}
