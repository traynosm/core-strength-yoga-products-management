using core_strength_yoga_products_management.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace core_strength_yoga_products_management.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IStockAuditService _stockAuditService;

        public ReportsController(IStockAuditService stockAuditService)
        {
            _stockAuditService = stockAuditService;
        }
        public async Task<IActionResult> Index()
        {
            var stockAudits = await _stockAuditService.Get(2);

            return View(stockAudits);
        }
        [HttpPost]
        public async Task<IActionResult> SearchByUsername(IFormCollection form)
        {
            DateTime.TryParse(form["date-start"], out var startDate);
            DateTime.TryParse(form["date-end"], out var endDate);
            var username = form["username"];

            var stockAudits = await _stockAuditService.SearchByUsername(
                username, startDate, endDate);
            
            return View("Index", stockAudits);

        }
    }
}
