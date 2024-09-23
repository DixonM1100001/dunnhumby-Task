using ProductManagementAPI.Mappers;
using ProductManagementAPI.Repositories;
using ProductManagementAPI.Services.Models;

namespace ProductManagementAPI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public Task<Product> CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var productsData = await _productsRepository.GetAllProductsAsync();
            var products = ProductMapper.ToProductModel(productsData);
            return products;
        }
    }
}
