using Microsoft.Identity.Client;

namespace core_strength_yoga_products_management.Models
{
    public class StockAudit
    {
        public int Id { get; set; }
        public DateTime ChangedAt { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int ProductAttributeId { get; set; }
        public virtual ProductAttributes ProductAttributes { get; set; }
        public string Username { get; set; }
        public int OldStockLevel { get; set; }
        public int NewStockLevel { get; set; }
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int StockLevelChange { get; set; }

        public StockAudit() { }
        public static int CalculateStockChange(int newStockLevel, int oldStockLevel)
        {
            return newStockLevel - oldStockLevel;
        }
    }
}
