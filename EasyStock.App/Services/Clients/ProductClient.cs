using EasyStock.Library.Entities;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using System;

namespace EasyStock.App.Services.Clients
{
    public class ProductClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductClient> _logger;

        public ProductClient(HttpClient httpClient, ILogger<ProductClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>?> GetProductsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all products");
                var products = await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("/product");
                _logger.LogInformation("Successfully fetched all products");
                return products;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching all products from API.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all products.");
                return null;
            }
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            try
            {
                _logger.LogInformation($"Fetching product details for ID: {productId}");
                var product = await _httpClient.GetFromJsonAsync<Product>($"/product/{productId}");
                if (product != null)
                {
                    _logger.LogInformation($"Successfully fetched product details for ID: {productId}");
                }
                else
                {
                    _logger.LogWarning($"Product with ID: {productId} not found.");
                }
                return product;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Error fetching product details from API for ID: {productId}.");
                return null; // It's often better to let exceptions propagate up where they can be handled or logged appropriately.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while fetching product details for ID: {productId}.");
                return null; // Propagate unexpected exceptions.
            }
        }
    }
}
