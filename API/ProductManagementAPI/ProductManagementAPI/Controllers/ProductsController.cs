using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Contracts;
using ProductManagementAPI.Mappers;
using ProductManagementAPI.Services;
using ProductManagementAPI.Services.Models;
using ProductManagementAPI.Validators;

namespace ProductManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productsService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductRequest productRequest)
        {
            var validator = new ProductRequestValidator(_productsService);
            var validationResult = await validator.ValidateAsync(productRequest);

            if (validationResult.IsValid)
            {
                var productToInsert = ProductMapper.ToProductModel(productRequest);
                var insertedProduct = await _productsService.CreateProductAsync(productToInsert);
                return Ok(insertedProduct);
            }

            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}
