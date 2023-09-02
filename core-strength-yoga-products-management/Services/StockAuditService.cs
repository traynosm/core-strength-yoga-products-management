using core_strength_yoga_products_management.Areas.Identity.Data;
using core_strength_yoga_products_management.Interfaces;
using core_strength_yoga_products_management.Models;
using core_strength_yoga_products_management.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NuGet.Common;

namespace core_strength_yoga_products_management.Services
{
    public class StockAuditService :IStockAuditService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ApiSettings> _settings;
        private readonly UserManager<ManagementUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<StockAuditService> _logger;
        private readonly IProductService _productService;

        public StockAuditService(HttpClient httpClient, IOptions<ApiSettings> settings, UserManager<ManagementUser> userManager, ITokenService tokenService, ILogger<StockAuditService> logger, IProductService productService)
        {
            _httpClient = httpClient;
            _settings = settings;
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
            _productService = productService;
            _httpClient.BaseAddress = new Uri(_settings.Value.BaseUrl);

        }

        public async Task<IEnumerable<StockAudit>> Get(int productId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<StockAudit>>(
                $"/api/v1/StockAudit/{productId}");
        }
        public async Task<IEnumerable<StockAudit>> FilterReport(
            string username, DateTime startDateTime, DateTime endDateTime, int productTypeId, int productId)
        {
            var usern = string.IsNullOrEmpty(username) ? "unknown" : username;

            var param = $"/api/v1/StockAudit" +
                $"/FilterReport" +
                $"/StartDateTime={startDateTime.ToString("yyyy-MM-ddTHH:mm:ss")}" +
                $"/EndDateTime={endDateTime.ToString("yyyy-MM-ddTHH:mm:ss")}" +
                $"/ProductId={productId}" +
                $"/ProductTypeId={productTypeId}" +
                $"/Username={usern.Replace(" ", "%20")}";

            //var param = $"/api/v1/StockAudit/SearchByUsername/Username=tit/StartDateTime=arse/EndDateTime=dick";

            return await _httpClient.GetFromJsonAsync<IEnumerable<StockAudit>>(param);

        }
    }
}
