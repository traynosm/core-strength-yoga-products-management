using core_strength_yoga_products_management.Models;

namespace core_strength_yoga_products_management.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order>? AllOrders { get; }

        Task<IEnumerable<Order>> GetOrders();
    }
}
