using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Models
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public string Address { get; set; } 
        public string Role { get; set; } 
    }
}
