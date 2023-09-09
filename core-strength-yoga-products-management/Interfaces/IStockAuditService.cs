using core_strength_yoga_products_management.Models;

namespace core_strength_yoga_products_management.Interfaces
{
    public interface IStockAuditService
    {
        Task<IEnumerable<StockAudit>?> Get();
        Task<IEnumerable<StockAudit>?> Get(int productId);
    }
}
