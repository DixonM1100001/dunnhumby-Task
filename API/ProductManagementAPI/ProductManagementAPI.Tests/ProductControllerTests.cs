using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagementAPI.Contracts;
using ProductManagementAPI.Controllers;
using ProductManagementAPI.Mappers;
using ProductManagementAPI.Models.Enums;
using ProductManagementAPI.Services;
using ProductManagementAPI.Services.Models;

namespace ProductManagementAPI.Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private ProductController _controller;

        const string uniqueString = "Unique";
        const string nonUniqueString = "NonUnique";

        static int counter = 1;

        public ProductControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductController( _mockProductService.Object );
        }


        #region GetAllProducts

        [Fact]
        public async void TestGetAllProductsReturnsSuccessWhenProductsFound()
        {
            //Arrange
            var testProducts = GetTestProducts();
            _mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(testProducts);

            //Act
            var response = await _controller.GetAllProducts();

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Result is OkObjectResult);

            var products = ((OkObjectResult)response.Result).Value as Product[];

            Assert.NotNull(products);

            for (var i = 0; i < products.Length; i++)
            {
                Assert.Equal(products[i].Name, testProducts[i].Name);
            }
        }

        [Fact]
        public async void TestGetAllProductsReturnsSuccessWhenNoProductsFound()
        {
            //Arrange
            _mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(Array.Empty<Product>());

            //Act
            var response = await _controller.GetAllProducts();

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Result is OkObjectResult);

            var products = ((OkObjectResult)response.Result).Value as Product[];

            Assert.NotNull(products);
            Assert.True(products.Length == 0);
        }

        [Fact]
        public async void TestGetAllProductsReturns500ErrorWhenExceptionThrow()
        {
            //Arrange
            _mockProductService.Setup(x => x.GetAllProductsAsync()).ThrowsAsync(new Exception());

            //Act
            var response = await _controller.GetAllProducts();

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Result is ObjectResult);

            var result = (ObjectResult)response.Result;

            Assert.Equal(result.StatusCode, 500);
        }

        #endregion

        #region CreateProduct

        [Fact]
        public async void TestCreateProductReturnsSuccess()
        {
            //Arrange
            var validTestProductRequest = GetValidTestProductRequest();
            var validTestProduct = ProductMapper.ToProductModel(validTestProductRequest);
            _mockProductService.Setup(x => x.CreateProductAsync(It.IsAny<Product>())).ReturnsAsync(validTestProduct);

            //Act
            var response = await _controller.CreateProduct(validTestProductRequest);

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Result is OkObjectResult);

            var insertedProduct = ((OkObjectResult)response.Result).Value as Product;

            Assert.NotNull(insertedProduct);

            Assert.Equal(validTestProductRequest.Name, validTestProduct.Name);
            Assert.Equal(validTestProductRequest.Category, validTestProduct.Category);
            Assert.Equal(validTestProductRequest.ProductCode, validTestProduct.ProductCode);
            Assert.Equal(validTestProductRequest.Price, validTestProduct.Price);
            Assert.Equal(validTestProductRequest.StockQuantity, validTestProduct.StockQuantity);
            Assert.Equal(validTestProductRequest.SKU, validTestProduct.SKU);
        }

        [Theory]
        [MemberData(nameof(InvalidProductRequest))]
        public async void TestCreateProductReturnsUnprocessableEntityWhenValidationFails(ProductRequest invalidProductRequest, string testName, List<Product> productsInDatabase)
        {
            //Arrange
            _mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(productsInDatabase);

            //Act
            var response = await _controller.CreateProduct(invalidProductRequest);

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Result is UnprocessableEntityObjectResult);

            var result = (UnprocessableEntityObjectResult)response.Result;

            Assert.NotNull(result.Value);
            Assert.NotEmpty((List<string>)result.Value);
        }

        [Fact]
        public async void TestCreateProductReturns500ErrorWhenExceptionThrown()
        {
            //Arrange
            var validTestProductRequest = GetValidTestProductRequest();
            var validTestProduct = ProductMapper.ToProductModel(validTestProductRequest);
            _mockProductService.Setup(x => x.CreateProductAsync(It.IsAny<Product>())).ThrowsAsync(new Exception());

            //Act
            var response = await _controller.CreateProduct(validTestProductRequest);

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Result is ObjectResult);

            var result = (ObjectResult)response.Result;

            Assert.Equal(result.StatusCode, 500);
        }

        #endregion

        #region Private Methods

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

        private ProductRequest GetValidTestProductRequest()
        {
            return new ProductRequest
            {
                Name = "Test",
                Category = Category.Food,
                Price = 1.23M,
                ProductCode = "Test1",
                SKU = GetUniqueString(),
                StockQuantity = 1
            };
        }

        public static IEnumerable<object[]> InvalidProductRequest()
        {
            yield return new object[] { new ProductRequest {
                Category = (Category)5, Name = GetUniqueString(), ProductCode = GetUniqueString(), SKU = GetUniqueString(), Price=1.11M, StockQuantity=1
            }, "Test Invalid Category", new List<Product>() };
            yield return new object[] { new ProductRequest {
                Category = (Category)1, Name = "", ProductCode = GetUniqueString(), SKU = GetUniqueString(), Price=1.11M, StockQuantity=1
            }, "Test Empty Name", new List<Product>() };
            yield return new object[] { new ProductRequest {
                Category = (Category)1, Name = GetUniqueString(), ProductCode = "", SKU = GetUniqueString(), Price=1.11M, StockQuantity=1
            }, "Test Empty Product Code", new List<Product>() };
            yield return new object[] { new ProductRequest {
                Category = (Category)1, Name = GetUniqueString(), ProductCode = GetUniqueString(), SKU = GetUniqueString(), Price=-1.11M, StockQuantity=1
            }, "Test Price Less than 0", new List<Product>() };
            yield return new object[] { new ProductRequest {
                Category = (Category)1, Name = GetUniqueString(), ProductCode = GetUniqueString(), SKU = GetUniqueString(), Price=1.111M, StockQuantity=1
            }, "Test Price has more than two decimal places", new List<Product>() };
            yield return new object[] { new ProductRequest {
                Category = (Category)1, Name = GetUniqueString(), ProductCode = GetUniqueString(), SKU = "", Price=1.11M, StockQuantity=1
            }, "Test Empty SKU", new List<Product>() };
            yield return new object[] { new ProductRequest {
                Category = (Category)1, Name = GetUniqueString(), ProductCode = GetUniqueString(), SKU = GetUniqueString(), Price=1.11M, StockQuantity=-1
            }, "Test StockQuantity Less than 0", new List<Product>() };
            yield return new object[] { 
                new ProductRequest {
                Category = (Category)1, Name = GetUniqueString(), ProductCode = GetUniqueString(), SKU = nonUniqueString, Price=1.11M, StockQuantity=-1
            }, "Test SKU already in database",
                    new List<Product> { new() {
                        SKU = nonUniqueString,
                        ProductCode = GetUniqueString(),
                        Name = GetUniqueString()
                    }
                }
            };
            yield return new object[] {
                new ProductRequest {
                Category = (Category)1, Name = GetUniqueString(), ProductCode = nonUniqueString, SKU = GetUniqueString(), Price=1.11M, StockQuantity=-1
            }, "Test ProductCode already in database",
                    new List<Product> { new() {
                        SKU = GetUniqueString(),
                        ProductCode = nonUniqueString,
                        Name = GetUniqueString()
                    }
                }
            };
        }

        private static string GetUniqueString()
        {
            ++counter;

            return uniqueString + counter.ToString();
        }

        #endregion
    }
}