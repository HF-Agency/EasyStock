using EasyStock.Library.Entities;
using System.Net.Http.Json;

namespace EasyStock.Library.Services.Clients
{
    public class ProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("api/product");
        }
    }
}
