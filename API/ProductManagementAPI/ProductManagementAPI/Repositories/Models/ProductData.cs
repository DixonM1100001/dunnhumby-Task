using ProductManagementAPI.Models.Enums;
using SQLite;

namespace ProductManagementAPI.Repositories.Models
{
    [Table("Products")]
    public class ProductData
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Category")]
        public Category Category { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("ProductCode")]
        public string ProductCode { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("SKU")]
        public string SKU { get; set; }

        [Column("StockQuantity")]
        public int StockQuantity { get; set; }

        [Column("DateAdded")]
        public DateTime DateAdded { get; set; }
    }
}
