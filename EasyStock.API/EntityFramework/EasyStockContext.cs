using EasyStock.Library.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace EasyStock.API.EntityFramework
{
    public class EasyStockContext : DbContext
    {
        private readonly IConfiguration config;

        public EasyStockContext(IConfiguration config)
        {
            this.config = config;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config["ConnectionString"]);
        }
    }
}
