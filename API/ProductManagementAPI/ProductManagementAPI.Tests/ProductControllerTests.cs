using FluentValidation;
using FluentValidation.Results;
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
        private readonly Mock<IValidator<ProductRequest>> _mockValidator;
        
        private ProductController _controller;

        public ProductControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _mockValidator = new Mock<IValidator<ProductRequest>>();
            _controller = new ProductController( _mockProductService.Object, _mockValidator.Object);
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

            _mockValidator.Setup(x => x.ValidateAsync(validTestProductRequest, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult
            {
                Errors = new List<ValidationFailure>()
            });
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

        [Fact]
        public async void TestCreateProductReturnsUnprocessableEntityWhenValidationFails()
        {
            //Arrange
            var invalidProductRequest = new ProductRequest { Name = "Name", ProductCode = "NAM1", SKU = "SKU" };

            _mockValidator.Setup(x => x.ValidateAsync(invalidProductRequest, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult
            {
                Errors = new List<ValidationFailure> { new ValidationFailure() }
            });

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


            _mockValidator.Setup(x => x.ValidateAsync(validTestProductRequest, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult
            {
                Errors = new List<ValidationFailure>()
            });

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
                SKU = "SKU",
                StockQuantity = 1
            };
        }

        #endregion
    }
}