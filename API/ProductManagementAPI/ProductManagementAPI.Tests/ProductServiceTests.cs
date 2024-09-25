using Moq;
using ProductManagementAPI.Controllers;
using ProductManagementAPI.Mappers;
using ProductManagementAPI.Models.Enums;
using ProductManagementAPI.Repositories;
using ProductManagementAPI.Repositories.Models;
using ProductManagementAPI.Services;
using ProductManagementAPI.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementAPI.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private IProductService _service;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _service = new ProductService(_mockProductRepository.Object);
        }

        #region GetAllProductsAsync

        [Fact]
        public async void TestGetAllProductsAsyncReturnsAllProductsInRepository()
        {
            //Arrange
            var testProducts = GetTestProducts();
            var testProductData = testProducts.Select(ProductMapper.ToProductDataModel);

            _mockProductRepository.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(testProductData);

            //Act
            var products = (await _service.GetAllProductsAsync()).ToArray();

            //Assert
            var productProperties = products[0].GetType().GetProperties();

            for (var i = 0; i < products.Count(); i++)
            {
                foreach (var productProperty in productProperties)
                {
                    Assert.Equal(productProperty.GetValue(products[i]), productProperty.GetValue(testProducts[i]));
                }
            }
        }

        #endregion

        #region CreateProductAsync

        [Fact]
        public async void TestCreateProductAsyncReturnsProductAfterCreation()
        {
            //Arrange
            var testProductToCreate = GetTestProduct();
            var testProductData = ProductMapper.ToProductDataModel(testProductToCreate);

            _mockProductRepository.Setup(x => x.CreateProductAsync(It.IsAny<ProductData>())).ReturnsAsync(testProductData);

            //Act
            var product = await _service.CreateProductAsync(testProductToCreate);

            //Assert
            var productProperties = product.GetType().GetProperties();

            foreach (var productProperty in productProperties)
            {
                Assert.Equal(productProperty.GetValue(product), productProperty.GetValue(testProductToCreate));
            }
        }

        #endregion

        #region PrivateMethods

        private Product[] GetTestProducts()
        {
            var testProducts = new List<Product>
            {
                new Product
                {
                    Category = Category.Food,
                    Name = "Test",
                    ProductCode = "Test1",
                    Price = 1.11M,
                    SKU = "UniqueSKU"
                }
            };

            return testProducts.ToArray();
        }

        private Product GetTestProduct()
        {
            var testProducts = new Product
            {
                Category = Category.Food,
                Name = "Test",
                ProductCode = "Test1",
                Price = 1.11M,
                SKU = "UniqueSKU"
            };

            return testProducts;
        }

        #endregion
    }
}
