using core_strength_yoga_products_management.Interfaces;
using core_strength_yoga_products_management.Models;
using core_strength_yoga_products_management.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace core_strength_yoga_products_management.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IStockAuditService _stockAuditService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public ReportsController(IStockAuditService stockAuditService, IOrderService orderService, IProductService productService)
        {
            _stockAuditService = stockAuditService;
            _orderService = orderService;
            _productService = productService;
        }

        [Authorize(Roles = "Admin, BusinessAnalyst")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin, BusinessAnalyst")]
        [HttpGet("Reports/Audit")]
        public async Task<IActionResult> Audit()
        {
            var stockAudits = await _stockAuditService.Get();

            return View(stockAudits!.OrderBy(a => a.ProductAttributeId));
        }

        [Authorize(Roles = "Admin, BusinessAnalyst")]
        [HttpGet("Reports/StockChanges/{productId}/{productAttributeId}")]
        public async Task<IActionResult> StockChanges(int productId, int productAttributeId)
        {
            var stockAudits = await _stockAuditService.Get(productId);

            stockAudits = stockAudits!.Where(a => 
                a.ProductAttributeId == productAttributeId);

            return View(stockAudits);
        }

        [Authorize(Roles = "Admin, BusinessAnalyst")]
        [HttpGet("Reports/Sales")]
        public async Task<IActionResult> Sales()
        {
            var orders = await _orderService.GetOrders();

            var basketItems = await _orderService.GetBasketItems();

            var products = _productService.AllProducts;

            var toRemove = new List<BasketItem>();

            foreach(var basketItem in basketItems) 
            { 
                var product = products.FirstOrDefault(p => p.Id == basketItem.ProductId);

                if(product == null)
                {
                    toRemove.Add(basketItem);
                }
            }

            basketItems = basketItems.Except(toRemove);

            return View(basketItems);
        }

        [Authorize(Roles = "Admin, BusinessAnalyst")]
        public static List<SelectListItem> BuildSelectItemsGender(int id)
        {
            var selectListItems = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(Gender)))
            {
                selectListItems.Add(new SelectListItem
                {
                    Value = item.ToString(),
                    Text = item.ToString(),
                    Selected = id == (int)item
                });
            }
            return selectListItems;
        }

        [Authorize(Roles = "Admin, BusinessAnalyst")]
        public static List<SelectListItem> BuildSelectItemsSize(int id)
        {
            var selectListItems = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(Size)))
            {
                selectListItems.Add(new SelectListItem
                {
                    Value = item.ToString(),
                    Text = item.ToString(),
                });
            }
            return selectListItems;
        }

        [Authorize(Roles = "Admin, BusinessAnalyst")]
        public static List<SelectListItem> BuildSelectItemsColour(int id)
        {
            var selectListItems = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(Colour)))
            {
                selectListItems.Add(new SelectListItem
                {
                    Value = item.ToString(),
                    Text = item.ToString(),
                });
            }
            return selectListItems;
        }
    }
}
