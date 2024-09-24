using ProductManagementAPI.Models.Enums;

namespace ProductManagementAPI.Services.Models
{
    public class Product
    {
        public Category Category { get; set; }

        public required string Name { get; set; }

        public required string ProductCode { get; set; }

        public decimal Price { get; set; }
        public string PriceSterling
        {
            get
            {
                return '£' + Price.ToString("F2");
            }
        }

        public required string SKU { get; set; }

        public int StockQuantity { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
