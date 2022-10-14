using System;
using System.Collections.Generic;

namespace ProjectAPI.Data.EFModels
{
    public partial class Role
    {
        public int? UserId { get; set; }
        public string? Roles { get; set; }
        public bool Status { get; set; }
        public virtual Registration? User { get; set; }
        public int RoleId { get; set; }
    }
}
