namespace EasyStock.Library.Entities.Interface
{
    public interface IProductClient
    {
        Task<IEnumerable<Product>?> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(int productId, Product product);
        Task DeleteProductAsync(int productId);
    }
}
