using EasyStock.Library.Entities;
using System.Net.Http.Json;

namespace EasyStock.App.Services.Clients
{
    public class ProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product?>?> GetProducts()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("api/product");
        }


        public async Task<Product> GetProductByIdAsync(int productId)
        {
            
             // Faça uma solicitação HTTP GET para obter os dados do produto
             var product = await _httpClient.GetFromJsonAsync<Product>($"api/product/{productId}");

             return product;
           
        }

 
    }
}

