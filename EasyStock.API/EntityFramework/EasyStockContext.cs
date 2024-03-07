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
            optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=EasyStockDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
