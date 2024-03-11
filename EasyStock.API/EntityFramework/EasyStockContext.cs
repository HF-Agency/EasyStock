using EasyStock.Library.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace EasyStock.API.EntityFramework
{
    public class EasyStockContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:hfagency.database.windows.net,1433;Initial Catalog=EasyStockDB;Persist Security Info=False;User ID=sqladmin;Password=EasyStock2024.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
