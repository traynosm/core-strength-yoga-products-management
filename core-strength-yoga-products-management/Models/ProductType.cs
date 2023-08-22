namespace core_strength_yoga_products_management.Models
{
    public class ProductType
    {
        public ProductType()
        {
            ProductTypeName = string.Empty;
        }
        public int Id { get; set; }
        public string ProductTypeName { get; set; }
        public string? Description { get; set; }
        public Image? Image { get; set; }
    }
}
