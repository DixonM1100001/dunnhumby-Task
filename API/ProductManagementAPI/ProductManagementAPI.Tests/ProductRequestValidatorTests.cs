using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagementAPI.Contracts;
using ProductManagementAPI.Controllers;
using ProductManagementAPI.Models.Enums;
using ProductManagementAPI.Services;
using ProductManagementAPI.Services.Models;
using ProductManagementAPI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementAPI.Tests
{
    public class ProductRequestValidatorTests
    {
        private readonly Mock<IProductService> _mockProductService;

        private ProductRequestValidator _validator;

        public ProductRequestValidatorTests()
        {
            _mockProductService = new Mock<IProductService>();
            _validator = new ProductRequestValidator(_mockProductService.Object);
        }

        [Fact]
        public async void TestValidationShouldFailWhenCategoryIsInvalid()
        {
            //Arrange
            var productRequest = new ProductRequest
            {
                Category = (Category)5,
                Name = "Name",
                ProductCode = "PROD1",
                SKU = "SKU",
                Price = 1.11M,
                StockQuantity = 1
            };

            _mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(new List<Product>());

            //Act
            var result = await _validator.ValidateAsync(productRequest);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void TestValidationShouldFailWhenNameIsEmpty()
        {
            //Arrange
            var productRequest = new ProductRequest
            {
                Category = (Category)1,
                Name = "",
                ProductCode = "PROD1",
                SKU = "SKU",
                Price = 1.11M,
                StockQuantity = 1
            };

            _mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(new List<Product>());

            //Act
            var result = await _validator.ValidateAsync(productRequest);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void TestValidationShouldFailWhenProductCodeIsEmpty()
        {
            //Arrange
            var productRequest = new ProductRequest
            {
                Category = (Category)1,
                Name = "Name",
                ProductCode = "",
                SKU = "SKU",
                Price = 1.11M,
                StockQuantity = 1
            };

            _mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(new List<Product>());

            //Act
            var result = await _validator.ValidateAsync(productRequest);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void TestValidationShouldFailWhenPriceLessThan0()
        {
            //Arrange
            var productRequest = new ProductRequest
            {
                Category = (Category)1,
                Name = "Name",
                ProductCode = "PROD1",
                SKU = "SKU",
                Price = -1.11M,
                StockQuantity = 1
            };

            _mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(new List<Product>());

            //Act
            var result = await _validator.ValidateAsync(productRequest);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void TestValidationShouldFailWhenPriceHasMoreThan2DecimalPlaces()
        {
            //Arrange
            var productRequest = new ProductRequest
            {
                Category = (Category)1,
                Name = "Name",
                ProductCode = "PROD1",
                SKU = "SKU",
                Price = 1.111M,
                StockQuantity = 1
            };

            _mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(new List<Product>());

            //Act
            var result = await _validator.ValidateAsync(productRequest);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void TestValidationShouldFailWhenSKUIsEmpty()
        {
            //Arrange
            var productRequest = new ProductRequest
            {
                Category = (Category)1,
                Name = "Name",
                ProductCode = "PROD1",
                SKU = "",
                Price = 1.11M,
                StockQuantity = 1
            };

            _mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(new List<Product>());

            //Act
            var result = await _validator.ValidateAsync(productRequest);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void TestValidationShouldFailWhenStockQuantityIsLessThan0()
        {
            //Arrange
            var productRequest = new ProductRequest
            {
                Category = (Category)1,
                Name = "Name",
                ProductCode = "PROD1",
                SKU = "SKU",
                Price = 1.11M,
                StockQuantity = -1
            };

            _mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(new List<Product>());

            //Act
            var result = await _validator.ValidateAsync(productRequest);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void TestValidationShouldFailWhenSKUAlreadyInDatabase()
        {
            //Arrange
            var productRequest = new ProductRequest
            {
                Category = (Category)1,
                Name = "Name",
                ProductCode = "PROD1",
                SKU = "NonUnique",
                Price = 1.11M,
                StockQuantity = 1
            };

            _mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(new List<Product>
            {
                new Product
                {
                    Name = "Unique",
                    ProductCode = "Unique",
                    SKU = "NonUnique"
                }
            });

            //Act
            var result = await _validator.ValidateAsync(productRequest);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void TestValidationShouldFailWhenProductCodeAlreadyInDatabase()
        {
            //Arrange
            var productRequest = new ProductRequest
            {
                Category = (Category)1,
                Name = "Name",
                ProductCode = "NonUnique",
                SKU = "SKU",
                Price = 1.11M,
                StockQuantity = 1
            };

            _mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(new List<Product>
            {
                new Product
                {
                    Name = "Unique",
                    ProductCode = "NonUnique",
                    SKU = "Unique"
                }
            });

            //Act
            var result = await _validator.ValidateAsync(productRequest);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async void TestValidationShouldPassWhenProductRequestValid()
        {
            //Arrange
            var productRequest = new ProductRequest
            {
                Category = (Category)1,
                Name = "Name",
                ProductCode = "PROD1",
                SKU = "SKU",
                Price = 1.11M,
                StockQuantity = 1
            };

            _mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(new List<Product>());

            //Act
            var result = await _validator.ValidateAsync(productRequest);

            //Assert
            Assert.True(result.IsValid);
        }
    }
}
