using EasyStock.Library.Entities;
using EasyStock.Library.Entities.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace EasyStock.API.EntityFramework
{
    public class EasyStockContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public EasyStockContext(DbContextOptions<EasyStockContext> options)
            :base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
