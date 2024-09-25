using ProductManagementAPI.Contracts;
using ProductManagementAPI.Repositories.Models;
using ProductManagementAPI.Services.Models;

namespace ProductManagementAPI.Mappers
{
    public static class ProductMapper
    {
        public static ProductData ToProductDataModel(this Product product)
        {
            return new ProductData
            {
                Category = product.Category,
                Name = product.Name,
                ProductCode = product.ProductCode,
                Price = product.Price,
                SKU = product.SKU,
                StockQuantity = product.StockQuantity,
                DateAdded = product.DateAdded
            };
        }

        public static Product ToProductModel(this ProductData productData)
        {
            return new Product
            {
                Category = productData.Category,
                Name = productData.Name,
                ProductCode = productData.ProductCode,
                Price = productData.Price,
                SKU = productData.SKU,
                StockQuantity = productData.StockQuantity,
                DateAdded = productData.DateAdded
            };
        }

        public static IEnumerable<Product> ToProductModel(this IEnumerable<ProductData> productsData)
        {
            return productsData.Select(ToProductModel);
        }

        public static Product ToProductModel(this ProductRequest productRequest)
        {
            return new Product
            {
                Category = productRequest.Category,
                Name = productRequest.Name,
                ProductCode = productRequest.ProductCode,
                Price = productRequest.Price,
                SKU = productRequest.SKU,
                StockQuantity = productRequest.StockQuantity,
                DateAdded = DateTime.UtcNow
            };
        }
    }
}
