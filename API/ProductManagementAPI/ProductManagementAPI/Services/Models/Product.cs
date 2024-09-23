using ProductManagementAPI.Models.Enums;

namespace ProductManagementAPI.Services.Models
{
    public class Product
    {
        public Category Category { get; set; }

        public string Name { get; set; }

        public string ProductCode { get; set; }

        public decimal Price { get; set; }

        public string SKU { get; set; }

        public int StockQuantity { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? LastUpdatedDate { get; set; }
    }
}
