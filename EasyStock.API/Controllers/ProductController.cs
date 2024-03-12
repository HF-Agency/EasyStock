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
    }
}

using System;
using System.Net.Http;
using System.Threading.Tasks;


public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        // Construa o URL do endpoint da API com o ID do produto
        string apiUrl = $"https://         .com/products/{productId}";     #por nome da api

        try
        {
            // Faça uma solicitação HTTP GET para obter os dados do produto
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            // Verifique se a solicitação foi bem-sucedida
            if (response.IsSuccessStatusCode)
            {
                // Leia os dados da resposta como uma string
                string responseData = await response.Content.ReadAsStringAsync();

                // Desserializar os dados da resposta para um objeto Product
                Product product = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(responseData);

                return product;
            }
            else
            {
                // Trate o caso de falha na solicitação (por exemplo, registro de erro)
                Console.WriteLine($"Falha ao obter o produto. Código de status: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            // Trate exceções (por exemplo, registro de erro)
            Console.WriteLine($"Erro ao obter o produto: {ex.Message}");
            return null;
        }
    }
}