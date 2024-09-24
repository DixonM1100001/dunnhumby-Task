using FluentValidation;
using ProductManagementAPI.Contracts;
using ProductManagementAPI.Services;

namespace ProductManagementAPI.Validators
{
    public class ProductRequestValidator : AbstractValidator<ProductRequest>
    {
        private readonly IProductsService _productsService;
        public ProductRequestValidator(IProductsService productsService) {
            _productsService = productsService;

            RuleFor(x => x.Category).IsInEnum();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ProductCode).NotEmpty();
            RuleFor(x => x.ProductCode).MustAsync(async (productCode, cancellation) =>
            {
                var products = await _productsService.GetAllProductsAsync();
                var productCodeExists = products.Any(x => x.ProductCode == productCode);
                return !productCodeExists;
            }).WithMessage("Product Code must be unique");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Price).PrecisionScale(int.MaxValue, 2, false).WithMessage("The price must not have more than 2 digits after the decimal and no trailling 0s");
            RuleFor(x => x.SKU).NotEmpty();
            RuleFor(x => x.SKU).MustAsync(async (sku, cancellation) =>
            {
                var products = await _productsService.GetAllProductsAsync();
                var skuExists = products.Any(x => x.SKU == sku);
                return !skuExists;
            }).WithMessage("SKU must be unique");
            RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0);
        }
    }
}
