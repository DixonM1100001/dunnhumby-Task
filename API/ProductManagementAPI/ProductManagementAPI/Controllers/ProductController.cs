using Microsoft.AspNetCore.Http.HttpResults;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productsService;

        public ProductController(IProductService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try
            {
                var products = await _productsService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductRequest productRequest)
        {
            var validator = new ProductRequestValidator(_productsService);
            var validationResult = await validator.ValidateAsync(productRequest);

            if (validationResult.IsValid)
            {
                try
                {
                    var productToInsert = ProductMapper.ToProductModel(productRequest);
                    var insertedProduct = await _productsService.CreateProductAsync(productToInsert);
                    return Ok(insertedProduct);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return UnprocessableEntity(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}
