using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectAPI.Business.Models
{
    public class RoleModel
    {
        public string Roles { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
    }
}
