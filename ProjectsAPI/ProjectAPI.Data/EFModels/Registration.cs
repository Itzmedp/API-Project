using System;
using System.Collections.Generic;

namespace ProjectAPI.Data.EFModels
{
    public partial class Registration
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set; }
        public string? Password { get; set; }
    }
}
