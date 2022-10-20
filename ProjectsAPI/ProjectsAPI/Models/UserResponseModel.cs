﻿namespace ProjectsAPI.Models
{
    public class UserResponseModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
    }
}
