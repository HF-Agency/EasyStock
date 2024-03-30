using EasyStock.Library.Entities;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using System;
using EasyStock.Library.Entities.Interface;

namespace EasyStock.App.Services.Clients
{
    public class ProductClient : IProductClient
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

        public async Task CreateProductAsync(Product product)
        {
            try
            {
                _logger.LogInformation("Creating a new product");
                var response = await _httpClient.PostAsJsonAsync("/product", product);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Product successfully created");
                }
                else
                {
                    _logger.LogWarning("Failed to create product. Status code: {StatusCode}", response.StatusCode);
                }
                response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response status is an error code.
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error creating product in API.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating a product.");
            }
        }

        public async Task UpdateProductAsync(int productId, Product product)
        {
            try
            {
                _logger.LogInformation($"Updating product with ID: {productId}");
                var response = await _httpClient.PutAsJsonAsync($"/product/{productId}", product);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Product with ID: {productId} successfully updated");
                }
                else
                {
                    _logger.LogWarning("Failed to update product with ID: {ProductId}. Status code: {StatusCode}", productId, response.StatusCode);
                }
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Error updating product in API for ID: {productId}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while updating product details for ID: {productId}.");
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            try
            {
                _logger.LogInformation($"Deleting product with ID: {productId}");
                var response = await _httpClient.DeleteAsync($"/product/{productId}");
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Product with ID: {productId} successfully deleted");
                }
                else
                {
                    _logger.LogWarning("Failed to delete product with ID: {ProductId}. Status code: {StatusCode}", productId, response.StatusCode);
                }
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Error deleting product from API for ID: {productId}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while deleting product for ID: {productId}.");
            }
        }

    }
}
