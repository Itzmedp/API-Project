using System;
using System.Collections.Generic;

namespace ProjectAPI.Data.EFModels
{
    public partial class Role
    {
        public int? UserId { get; set; }
        public string? Role1 { get; set; }
        public virtual Registration? User { get; set; }
    }
}
