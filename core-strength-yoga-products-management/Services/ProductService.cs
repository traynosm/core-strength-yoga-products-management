using core_strength_yoga_products_management.Interfaces;
using core_strength_yoga_products_management.Models;
using core_strength_yoga_products_management.Settings;
using Microsoft.Extensions.Options;
using ProductCategory = core_strength_yoga_products_management.Models.ProductCategory;

namespace core_strength_yoga_products_management.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ApiSettings> _settings;


        public ProductService(HttpClient httpClient, IOptions<ApiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _httpClient.BaseAddress = new Uri(_settings.Value.BaseUrl);

        }
        public async Task<IEnumerable<Product>?> GetProducts()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("/api/v1/Products");
        }

        public async Task<IEnumerable<ProductCategory>?> GetCategories()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ProductCategory>>("/api/v1/ProductCategories");
        }
        public async Task<IEnumerable<ProductType>?> GetTypes()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ProductType>>("/api/v1/ProductTypes");
        }
        public async Task<IEnumerable<Product>?> GetByProductCategory(int productCategoryId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Product>>($"/api/v1/Products/ByCategory/{productCategoryId}");
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"/api/v1/Products/{productId}"); 
        }
    }
}
