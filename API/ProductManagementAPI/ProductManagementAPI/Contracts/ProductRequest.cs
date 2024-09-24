using ProductManagementAPI.Models.Enums;

namespace ProductManagementAPI.Contracts
{
    public class ProductRequest
    {
        public Category Category { get; set; }
        public required string Name { get; set; }
        public required string ProductCode { get; set; }
        public decimal Price { get; set; }
        public required string SKU { get; set; }
        public int StockQuantity { get; set; }
    }
}
