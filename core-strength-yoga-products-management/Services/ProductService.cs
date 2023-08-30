using core_strength_yoga_products_management.Areas.Identity.Data;
using core_strength_yoga_products_management.Interfaces;
using core_strength_yoga_products_management.Models;
using core_strength_yoga_products_management.Models.Exceptions;
using core_strength_yoga_products_management.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using ProductCategory = core_strength_yoga_products_management.Models.ProductCategory;

namespace core_strength_yoga_products_management.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ApiSettings> _settings;
        private readonly UserManager<ManagementUser> _userManager;
        private readonly ITokenService _tokenService;

        public ProductService(HttpClient httpClient, IOptions<ApiSettings> settings, UserManager<ManagementUser> userManager,
            ITokenService tokenService)
        {
            _httpClient = httpClient;
            _settings = settings;
            _httpClient.BaseAddress = new Uri(_settings.Value.BaseUrl);
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<IEnumerable<Product>?> GetProducts()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("/api/v1/Products");
        }
        public async Task<Product?> GetProductById(int productId)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"/api/v1/Products/{productId}");
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
        public async Task<IEnumerable<Product>?> GetByProductType(int productTypeId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Product>>($"/api/v1/Products/ByType/{productTypeId}");
        }

        public async Task<Product?> Add(Product product)
        {
            if (!_tokenService.ValidateToken())
            {
                throw new InvalidTokenException();
            }
            var jwtToken = _tokenService.JwtToken;

            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (jwtToken != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", jwtToken);
            }

            var json = JsonConvert.SerializeObject(product);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"/api/v1/Products", content);

            if (!response.IsSuccessStatusCode)
            {
            }

            var updatedProductJson = await response.Content.ReadAsStringAsync();

            var updatedProduct = JsonConvert.DeserializeObject<Product>(updatedProductJson);

            return updatedProduct;
        }

        public async Task<Product?> Update(Product product)
        {
            if (!_tokenService.ValidateToken())
            {
                throw new InvalidTokenException();
            }

            var jwtToken = _tokenService.JwtToken;

            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (jwtToken != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", jwtToken);
            }

            var json = JsonConvert.SerializeObject(product);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/v1/Products", content);

            if(!response.IsSuccessStatusCode) 
            { 
            }

            var updatedProductJson = await response.Content.ReadAsStringAsync();

            var updatedProduct = JsonConvert.DeserializeObject<Product>(updatedProductJson);

            return updatedProduct;
        }
        public async Task<bool?> DeleteByProductId(int productId)
        {
            if (!_tokenService.ValidateToken())
            {
                throw new InvalidTokenException();
            }
            var jwtToken = _tokenService.JwtToken;

            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (jwtToken != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", jwtToken);
            }

            var response = await _httpClient.GetAsync($"/api/v1/Products/Delete/{productId}");

            if (!response.IsSuccessStatusCode)
            {
            }

            return true;

        }
    }
}
