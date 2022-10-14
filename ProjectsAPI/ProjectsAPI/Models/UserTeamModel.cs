using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectAPI.Models
{
    public class UserTeamModel
    {
        public string UserName { get; set; }
        public string TeamType { get; set; }
        public string AssignedUser { get; set; }

    }
}
