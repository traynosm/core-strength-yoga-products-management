using core_strength_yoga_products_management.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace core_strength_yoga_products_management.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IStockAuditService _stockAuditService;
        private readonly IProductService _productService;

        public ReportsController(IStockAuditService stockAuditService, IProductService productService)
        {
            _stockAuditService = stockAuditService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _productService.GetCategories();
            ViewData["categories"] = categories;

            var types = await _productService.GetTypes();
            ViewData["types"] = types;

            var products = await _productService.GetProducts();
            ViewData["products"] = products;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FilterReport(IFormCollection form)
        {
            DateTime.TryParse(form["date-start"], out var startDate);
            DateTime.TryParse(form["date-end"], out var endDate);
            var username = form["username"];
            var productTypeId = int.Parse(form["product-type"]);
            var productId = int.Parse(form["product"]);

            var stockAudits = await _stockAuditService.FilterReport(
                username, startDate, endDate, productTypeId, productId);

            var types = await _productService.GetTypes();
            ViewData["types"] = types;

            var products = await _productService.GetProducts();
            ViewData["products"] = products;

            ViewData["date-start"] = startDate.ToString("yyyy-MM-ddTHH:mm");
            ViewData["date-end"] = endDate.ToString("yyyy-MM-ddTHH:mm");
            ViewData["username"] = username;
            ViewData["product-type"] = productTypeId;
            ViewData["product"] = productId;

            //2018 - 06 - 12T19: 30
            return View("Index", stockAudits);

        }
    }
}
