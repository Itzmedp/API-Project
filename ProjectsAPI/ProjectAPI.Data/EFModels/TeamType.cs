using System;
using System.Collections.Generic;

namespace ProjectAPI.Data.EFModels
{
    public partial class TeamType
    {
        public TeamType()
        {
            UserTeams = new HashSet<UserTeam>();
        }

        public int TeamTypeId { get; set; }
        public string TeamType1 { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<UserTeam> UserTeams { get; set; }
    }
}
