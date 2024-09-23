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
                SKU = productData.ProductCode,
                StockQuantity = productData.StockQuantity,
                DateAdded = productData.DateAdded,
                LastUpdatedDate = productData.LastUpdatedDate,
            };
        }

        public static IEnumerable<Product> ToProductModel(this IEnumerable<ProductData> productsData)
        {
            return productsData.Select(p => p.ToProductModel());
        }
    }
}
