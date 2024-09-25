using ProductManagementAPI.Services.Models;

namespace ProductManagementAPI.Services
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}