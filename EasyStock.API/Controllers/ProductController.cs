using EasyStock.API.EntityFramework;
using EasyStock.Library.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyStock.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly EasyStockContext _context;

        public ProductController(EasyStockContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Product>> GetProducts()
        {
            // Get products from the database
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }
}
}