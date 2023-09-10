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
        private readonly IProductService _productService;

        public OrderService(HttpClient httpClient, IOptions<ApiSettings> settings,
            ILogger<OrderService> logger, IProductService productService)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
            _httpClient.BaseAddress = new Uri(_settings.Value.BaseUrl);
            _productService = productService;

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

        public async Task<IEnumerable<BasketItem>> GetBasketItems()
        {
            var orders = await GetOrders();

            var basketItems = orders.SelectMany(o => o.Items);

            foreach (var basketItem in basketItems)
            {
                if (basketItem.Colour == null)
                {
                    basketItem.Colour = _productService.AllProducts.FirstOrDefault(p =>
                        p.Id == basketItem.ProductId)?
                        .ProductAttributes.FirstOrDefault(pa => pa.Id == basketItem.ProductAttributeId)?
                        .Colour;
                }

                if (basketItem.Size == null)
                {
                    basketItem.Size = _productService.AllProducts.FirstOrDefault(p =>
                        p.Id == basketItem.ProductId)?
                        .ProductAttributes.FirstOrDefault(pa => pa.Id == basketItem.ProductAttributeId)?
                        .Size;
                }

                if (basketItem.Gender == null)
                {
                    basketItem.Gender = _productService.AllProducts.FirstOrDefault(p =>
                        p.Id == basketItem.ProductId)?
                        .ProductAttributes.FirstOrDefault(pa => pa.Id == basketItem.ProductAttributeId)?
                        .Gender;
                }

                if(basketItem.OrderDate == null)
                {
                    basketItem.OrderDate = orders.FirstOrDefault(o => o.Id == basketItem.OrderId)?.DateOfSale;
                }
            }
            return basketItems;

        }


    }
}
