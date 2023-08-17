using core_strength_yoga_products_management.Models;

namespace core_strength_yoga_products_management.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>?> GetProducts();
        Task<Product> GetProductById(int productId);
        Task<IEnumerable<ProductCategory>?> GetCategories();
        Task<IEnumerable<ProductType>?> GetTypes();
        Task<IEnumerable<Product>?> GetByProductCategory(int productCategoryId);
        Task<IEnumerable<Product>?> GetByProductType(int productTypeId);

    }
}
