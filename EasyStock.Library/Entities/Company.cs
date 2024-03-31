using EasyStock.Library.Entities.Authentication;

namespace EasyStock.Library.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property for ApplicationUsers
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
