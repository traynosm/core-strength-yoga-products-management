﻿namespace core_strength_yoga_products_management.Models
{
    public class Product
    {
        public Product() 
        { 
            Image = new Image(); 
        }
        public int Id { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ProductType ProductType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal FullPrice { get; set; }
        public Image? Image { get; set; }
        public IEnumerable<ProductAttributes> ProductAttributes { get; set; }
    }
}

