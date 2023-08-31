using core_strength_yoga_products_management.Models;

namespace core_strength_yoga_products_management.Interfaces
{
    public interface IStockAuditService
    {
        Task<IEnumerable<StockAudit>> Get(int productId);
        Task<IEnumerable<StockAudit>> FilterReport(
            string username, DateTime startDate, DateTime endDate);
    }
}
