using core_strength_yoga_products_management.Interfaces;
using core_strength_yoga_products_management.Models;
using core_strength_yoga_products_management.Settings;
using Microsoft.Extensions.Options;

namespace core_strength_yoga_products_management.Services
{
    public class StockAuditService : IStockAuditService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ApiSettings> _settings;
        private readonly ILogger<StockAuditService> _logger;
        private readonly IProductService _productService;

        public StockAuditService(HttpClient httpClient, IOptions<ApiSettings> settings, 
            ILogger<StockAuditService> logger, IProductService productService)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
            _productService = productService;
            _httpClient.BaseAddress = new Uri(_settings.Value.BaseUrl);

        }
        public async Task<IEnumerable<StockAudit>?> Get()
        {
            var stockAudits = await _httpClient.GetFromJsonAsync<IEnumerable<StockAudit>>(
                $"/api/v1/StockAudit");

            if( stockAudits == null)
            {
                return new List<StockAudit>();
            }

            AttachProducts(stockAudits);

            return stockAudits;
        }

        public async Task<IEnumerable<StockAudit>?> Get(int productId)
        {
            var stockAudits = await _httpClient.GetFromJsonAsync<IEnumerable<StockAudit>>(
                $"/api/v1/StockAudit/{productId}");

            if (stockAudits == null)
            {
                return new List<StockAudit>();
            }

            AttachProducts(stockAudits);

            return stockAudits;
        }

        private void AttachProducts(IEnumerable<StockAudit> stockAudits)
        {
            foreach(var audit in stockAudits)
            {
                var product = _productService.AllProducts.FirstOrDefault(p => p.Id == audit.ProductId);

                if(product != null)
                {
                    audit.Product = product;
                }
                else
                {
                    continue;
                }

                var productAttribute = product!.ProductAttributes.FirstOrDefault(p => p.Id == audit.ProductAttributeId);
                
                if (productAttribute != null)
                {
                    audit.ProductAttributes = productAttribute;
                }
                else
                {
                    continue;
                }

            }
        }
    }
}
