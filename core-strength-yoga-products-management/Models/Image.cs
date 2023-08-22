namespace core_strength_yoga_products_management.Models
{
    public class Image
    {
        public Image() { }
        public Image(int id) 
        {
            Id = id;
        }
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string? Alt { get; set; }
        public string? Path { get; set; }
        public int ProductId { get; set; }

    }
}
