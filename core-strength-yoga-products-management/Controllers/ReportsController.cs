﻿using core_strength_yoga_products_management.Interfaces;
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
            var stockAudits = await _stockAuditService.Get(2);

            return View(stockAudits);
        }
        [HttpPost]
        public async Task<IActionResult> FilterReport(IFormCollection form)
        {
            DateTime.TryParse(form["date-start"], out var startDate);
            DateTime.TryParse(form["date-end"], out var endDate);
            var username = form["username"];

            var stockAudits = await _stockAuditService.FilterReport(
                username, startDate, endDate);
            
            return View("Index", stockAudits);

        }
    }
}
