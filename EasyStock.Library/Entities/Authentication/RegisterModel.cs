﻿namespace EasyStock.Library.Entities.Authentication
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public int? CompanyId { get; set; }
    }
}
