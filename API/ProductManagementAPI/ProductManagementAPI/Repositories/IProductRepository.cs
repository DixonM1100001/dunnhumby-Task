using ProductManagementAPI.Repositories.Models;

namespace ProductManagementAPI.Repositories
{
    public interface IProductRepository
    {
        Task<ProductData> CreateProductAsync(ProductData product);
        Task<IEnumerable<ProductData>> GetAllProductsAsync();
    }
}