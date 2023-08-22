using System;

namespace core_strength_yoga_products_management.Models
{
    public class ProductAttributes
    {
        public ProductAttributes() { }

        public ProductAttributes(int id) 
        { 
            Id = id;
        }
        public int Id { get; set; }
        public int StockLevel { get; set; }
        public decimal PriceAdjustment { get; set; }
        public Enums.Colour Colour { get; set; }
        public Enums.Size Size { get; set; }
        public Enums.Gender Gender { get; set; }
    }
}
