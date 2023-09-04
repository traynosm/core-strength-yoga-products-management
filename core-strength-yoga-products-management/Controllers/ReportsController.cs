using core_strength_yoga_products_management.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return RedirectToAction("Audit");
        }

        [HttpGet("Reports/Audit")]
        public async Task<IActionResult> Audit()
        {
            var stockAudits = await _stockAuditService.Get();

            return View(stockAudits!.OrderBy(a => a.ProductAttributeId));
        }

        [HttpGet("Reports/StockChanges/{productId}/{productAttributeId}")]
        public async Task<IActionResult> StockChanges(int productId, int productAttributeId)
        {
            var stockAudits = await _stockAuditService.Get(productId);

            stockAudits = stockAudits!.Where(a => 
                a.ProductAttributeId == productAttributeId);

            return View(stockAudits);
        }
        [HttpGet("Reports/Sales")]
        public async Task<IActionResult> Sales()
        {
            var orders = await _orderService.GetOrders();

            var basketItems = orders.SelectMany(o => o.Items);

            var products = _productService.AllProducts;
            ViewData["products"] = products;

            ViewData["orders"] = orders;


            return View(basketItems);
        }
    }
}
