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

        public async Task<Product> CreateProductAsync(Product product)
        {
            var productDataModelToInsert = ProductMapper.ToProductDataModel(product);
            var insertedProductData = await _productsRepository.CreateProductAsync(productDataModelToInsert);
            var insertedProduct = ProductMapper.ToProductModel(insertedProductData);
            return insertedProduct;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var productsData = await _productsRepository.GetAllProductsAsync();
            var products = ProductMapper.ToProductModel(productsData);
            return products;
        }
    }
}
