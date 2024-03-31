using Microsoft.AspNetCore.Identity;

namespace EasyStock.Library.Entities.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public int CompanyId { get; set; }

        // Navigation property to the Company
        public Company Company { get; set; }
    }
}
