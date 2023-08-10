using core_strength_yoga_products_management.Models;

namespace core_strength_yoga_products_management.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>?> GetProducts();
    }
}
