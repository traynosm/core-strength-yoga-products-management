using core_strength_yoga_products_management.Interfaces;
using core_strength_yoga_products_management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace core_strength_yoga_products_management.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var categories = await _productService.GetCategories();
            ViewData["categories"] = categories;

            var products = await _productService.GetProducts();
            return View(products);
        }

        public async Task<IActionResult> GetCategory()
        {
            var categories = await _productService.GetCategories();
            return View(categories);
        }
        public async Task<IActionResult> GetType()
        {
            var types = await _productService.GetTypes();
            return View(types);
        }
        public async Task<IActionResult> GetByCategory(int productCategoryId)
        {
            var productsByCategories = await _productService.GetByProductCategory(productCategoryId);
            if (productsByCategories == null || !productsByCategories.Any())
            {
                return RedirectToAction("Index");
            }
            var categories = await _productService.GetCategories();
            ViewData["categories"] = categories;

            return View("Index", productsByCategories);
        }
        [HttpGet("Product/ProductById/{productId}")]
        public async Task<IActionResult> ProductById(int productId)
        {
            var product = await _productService.GetProductById(productId);


            var selectListItemsCategories = await BuildSelectItemsCategories(product.ProductCategory.Id);

            ViewData["selectListItemsCategories"] = selectListItemsCategories;

            var selectListItemsTypes = await BuildSelectItemsTypes(product.ProductType.Id);

            ViewData["selectListItemsTypes"] = selectListItemsTypes;


            return View(product);
        }
        public async Task<IActionResult> Update(Product product)
        {
            return View("ProductById", product);
        }

        private async Task<List<SelectListItem>> BuildSelectItemsCategories(int productCategoryId)
        {
            var categories = await _productService.GetCategories();
            var selectListItemsCategories = new List<SelectListItem>();
            foreach (var item in categories)
            {
                selectListItemsCategories.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.ProductCategoryName, Selected = productCategoryId == item.Id });
            }
            return selectListItemsCategories;
        }

        private async Task<List<SelectListItem>> BuildSelectItemsTypes(int productTypeId)
        {
            var types = await _productService.GetTypes();
            var selectListItemsTypes = new List<SelectListItem>();
            foreach (var item in types)
            {
                selectListItemsTypes.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.ProductTypeName, Selected = productTypeId == item.Id });
            }
            return selectListItemsTypes;
        }
    }
}