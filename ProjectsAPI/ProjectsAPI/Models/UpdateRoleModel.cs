using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Business.Models
{
    public class UpdateRoleModel
    {
        public string Roles { get; set; } = null!;
        public bool Status { get; set; }
    }
}
