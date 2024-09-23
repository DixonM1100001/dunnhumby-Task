using ProductManagementAPI.Repositories.Models;

namespace ProductManagementAPI.Repositories
{
    public interface IProductsRepository
    {
        Task<ProductData> CreateProductAsync(ProductData product);
        Task<IEnumerable<ProductData>> GetAllProductsAsync();
    }
}