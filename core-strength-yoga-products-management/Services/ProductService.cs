using core_strength_yoga_products_management.Areas.Identity.Data;
using core_strength_yoga_products_management.Interfaces;
using core_strength_yoga_products_management.Models;
using core_strength_yoga_products_management.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using ProductCategory = core_strength_yoga_products_management.Models.ProductCategory;

namespace core_strength_yoga_products_management.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ApiSettings> _settings;
        private readonly UserManager<core_strength_yoga_products_managementUser> _userManager;

        private string _jwt;

        public ProductService(HttpClient httpClient, IOptions<ApiSettings> settings, UserManager<core_strength_yoga_products_managementUser> userManager)
        {
            _httpClient = httpClient;
            _settings = settings;
            _httpClient.BaseAddress = new Uri(_settings.Value.BaseUrl);
            _userManager = userManager;

            _jwt = Login().Result;
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
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (_jwt != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _jwt);
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
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (_jwt != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _jwt);
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

        //private async Task<JwtSecurityToken> Login()
        private async Task<string> Login()
        {
            //var user = await _userManager.FindByEmailAsync("gavin.rudge@betfair.com");
            var model = new { Username = "admin@email.com", Password = "Testing123!" };
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await _httpClient.PostAsync("/api/v1/auth/login", content);
            string resultContent = await result.Content.ReadAsStringAsync();

            var jsonObject = JObject.Parse(resultContent);
            var tokenValue = jsonObject.GetValue("token").ToString();
            var tokenHandler = new JwtSecurityTokenHandler();

            // Read the JWT token
            var jwtToken = tokenHandler.ReadJwtToken(tokenValue);
            return tokenValue;
        }
    }
}
