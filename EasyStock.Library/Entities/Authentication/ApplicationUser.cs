using Microsoft.AspNetCore.Identity;

namespace EasyStock.Library.Entities.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int? CompanyId { get; set; }

        // Navigation property to the Company
        public Company Company { get; set; }
    }
}
