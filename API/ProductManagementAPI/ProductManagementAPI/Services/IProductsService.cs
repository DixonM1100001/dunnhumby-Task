using ProductManagementAPI.Services.Models;

namespace ProductManagementAPI.Services
{
    public interface IProductsService
    {
        Task<Product> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}