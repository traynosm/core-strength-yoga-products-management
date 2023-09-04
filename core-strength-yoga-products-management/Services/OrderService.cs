using core_strength_yoga_products_management.Interfaces;
using core_strength_yoga_products_management.Models;
using core_strength_yoga_products_management.Settings;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace core_strength_yoga_products_management.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ApiSettings> _settings;
        private readonly ILogger<OrderService> _logger;

        public OrderService(HttpClient httpClient, IOptions<ApiSettings> settings, ILogger<OrderService> logger)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
            _httpClient.BaseAddress = new Uri(_settings.Value.BaseUrl);

        }

        public IEnumerable<Order>? AllOrders { get; private set; }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var orders = await _httpClient.GetFromJsonAsync<IEnumerable<Order>>($"/api/v1/Order");

            if (orders == null)
            {
                return new List<Order>();
            }

            AllOrders = orders;

            return orders;
        }
    }


}
