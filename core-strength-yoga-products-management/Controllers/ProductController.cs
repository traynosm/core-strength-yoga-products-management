﻿using core_strength_yoga_products_management.Interfaces;
using core_strength_yoga_products_management.Models;
using core_strength_yoga_products_management.Models.Enums;
using core_strength_yoga_products_management.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using ProductCategory = core_strength_yoga_products_management.Models.ProductCategory;

namespace core_strength_yoga_products_management.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly ILoginService _loginService;
        public ProductController(IProductService productService, IWebHostEnvironment hostingEnvironment,
            ILoginService loginService)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
            _loginService = loginService;
        }

        // GET: ProductController
        [Authorize(Roles = "Admin, ProductManager, ProductExecutive")]
        public async Task<ActionResult> Index()
        {

            var categories = await _productService.GetCategories();
            ViewData["categories"] = categories;

            var types = await _productService.GetTypes();
            ViewData["types"] = types;

            var products = await _productService.GetProducts();

            return View(products);
        }

        [Authorize(Roles = "Admin, ProductManager, ProductExecutive")]

        public async Task<IActionResult> GetCategory()
        {
            var categories = await _productService.GetCategories();
            return View(categories);
        }

        [Authorize(Roles = "Admin, ProductManager, ProductExecutive")]

        public async Task<IActionResult> GetTypes()
        {
            var types = await _productService.GetTypes();
            return View(types);
        }

        [Authorize(Roles = "Admin, ProductManager, ProductExecutive")]

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

        [Authorize(Roles = "Admin, ProductManager, ProductExecutive")]

        public async Task<IActionResult> GetByType(int productTypeId)
        {
            var productsByType = await _productService.GetByProductType(productTypeId);
            if (productsByType == null || !productsByType.Any())
            {
                return RedirectToAction("Index");
            }
            var types = await _productService.GetTypes();
            ViewData["types"] = types;

            return View("Index", productsByType);
        }

        [Authorize(Roles = "Admin, ProductManager")]
        [HttpGet]
        public async Task<IActionResult> Add(Product? product)
        {            
            if(product == null)
            {
                product = new Product();
            }
            
            var selectListItemsCategories = await BuildSelectItemsCategories(0);
            ViewData["selectListItemsCategories"] = selectListItemsCategories;

            var selectListItemsTypes = await BuildSelectItemsTypes(0);
            ViewData["selectListItemsTypes"] = selectListItemsTypes;

            return View("Add", product);
        }

        [Authorize(Roles = "Admin, ProductManager, ProductExecutive")]
        [HttpGet("Product/ProductAttributePartialView")]
        public IActionResult ProductAttributePartialView()
        {
            return PartialView("ProductAttribute", new ProductAttributes());
        }

        [Authorize(Roles = "Admin, ProductManager, ProductExecutive")]
        [HttpGet("Product/Edit/{productId}")]
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _productService.GetProductById(productId);

            var selectListItemsCategories = await BuildSelectItemsCategories(product.ProductCategory.Id);
            ViewData["selectListItemsCategories"] = selectListItemsCategories;

            var selectListItemsTypes = await BuildSelectItemsTypes(product.ProductType.Id);
            ViewData["selectListItemsTypes"] = selectListItemsTypes;

            return View(product);
        }

        [Authorize(Roles = "Admin, ProductManager, ProductExecutive")]

        public async Task<IActionResult> AddOrUpdate(IFormCollection form)
        {
            var product = BuildProductFromFormCollection(form);

            if (!product.ProductAttributes.Any())
            {
                return RedirectToAction("Add", product);
            }

            var updatedProduct = new Product();

            try
            {
                if (product.Id == 0)
                {
                    updatedProduct = await _productService.Add(product);
                }
                else
                {
                    updatedProduct = await _productService.Update(product);
                }
            }
            catch(InvalidOperationException)
            {
                return RedirectToAction("Add", product);
            }
            catch(InvalidTokenException)
            {
                return RedirectToPage("~/Identity/Account/Login");
            }

            var selectListItemsCategories = await BuildSelectItemsCategories(product.ProductCategory.Id);
            ViewData["selectListItemsCategories"] = selectListItemsCategories;

            var selectListItemsTypes = await BuildSelectItemsTypes(product.ProductType.Id);
            ViewData["selectListItemsTypes"] = selectListItemsTypes;

            if(updatedProduct!.Id == 0)
            {
                return RedirectToAction("Add", product); 
            }
            else
            {
                return RedirectToAction("Edit", new { updatedProduct.Id });
            }
        }

        [Authorize(Roles = "Admin, ProductManager")]
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");

            if (image != null && image.Length > 0)
            {
                string filePath = Path.Combine(uploads, image.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                return Ok("{}");
            }
            
            return BadRequest();
        }

        [Authorize(Roles = "Admin, ProductManager")]
        [HttpGet("Product/DeleteProduct/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productid)
        {
            try
            {
                var deleted = await _productService.DeleteByProductId(productid);
            }
            catch (InvalidTokenException)
            {
                return Redirect("~/Identity/Account/Login");
            }

            return RedirectToAction("Index");
        }

        private async Task<List<SelectListItem>> BuildSelectItemsCategories(int productCategoryId)
        {
            var categories = await _productService.GetCategories() ?? 
                throw new NullReferenceException($"GetCategories responded with null");

            var selectListItemsCategories = new List<SelectListItem>();
            foreach (var item in categories)
            {
                selectListItemsCategories.Add(
                    new SelectListItem 
                    { 
                        Value = item.Id.ToString(), 
                        Text = item.ProductCategoryName, 
                        Selected = productCategoryId == item.Id 
                    });
            }
            return selectListItemsCategories;
        }

        private async Task<List<SelectListItem>> BuildSelectItemsTypes(int productTypeId)
        {
            var types = await _productService.GetTypes() ??
                throw new NullReferenceException($"GetTypes responded with null"); 

            var selectListItemsTypes = new List<SelectListItem>();
            foreach (var item in types)
            {
                selectListItemsTypes.Add(
                    new SelectListItem 
                    { 
                        Value = item.Id.ToString(), 
                        Text = item.ProductTypeName, 
                        Selected = productTypeId == item.Id 
                    });
            }
            return selectListItemsTypes;
        }

        [Authorize(Roles = "Admin, ProductManager, ProductExecutive")]
        public static List<SelectListItem> BuildSelectItemsGender(int id)
        {
            var selectListItems = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(Gender)))
            {
               selectListItems.Add(new SelectListItem 
               { 
                   Value = ((int)item).ToString(), 
                   Text = item.ToString(), 
                   Selected = id == (int)item 
               });
            }
            return selectListItems;
        }

        [Authorize(Roles = "Admin, ProductManager, ProductExecutive")]
        public static List<SelectListItem> BuildSelectItemsSize(int id)
        {
            var selectListItems = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(Size)))
            {
                selectListItems.Add(new SelectListItem 
                { 
                    Value = ((int)item).ToString(), 
                    Text = item.ToString(), 
                    Selected = id == (int)item
                });
            }
            return selectListItems;
        }

        [Authorize(Roles = "Admin, ProductManager, ProductExecutive")]
        public static List<SelectListItem> BuildSelectItemsColour(int id)
        {
            var selectListItems = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(Colour)))
            {
                selectListItems.Add(new SelectListItem
                {
                    Value = ((int)item).ToString(),
                    Text = item.ToString(),
                    Selected = id == (int)item
                });
            }
            return selectListItems;
        }

        private static List<Dictionary<string, object>> ExtractProductAttributes(IFormCollection form)
        {
            var index = 0;
            var productAttributes = new List<Dictionary<string, object>>();
            var singleAttribute = new Dictionary<string, object>();
            foreach (var val in form)
            {
                if (val.Key.Contains("Attr"))
                {
                    if (index > 0) productAttributes.Add(singleAttribute);
                    index++;
                    singleAttribute = new Dictionary<string, object>();
                }

                if (index > 0 && !val.Key.Contains("Token"))
                {
                    singleAttribute.Add(val.Key, val.Value);
                }
            }
            productAttributes.Add(singleAttribute);

            return productAttributes;
        }

        private static Product BuildProductFromFormCollection(IFormCollection form)
        {
            var product = new Product();
            product.ProductCategory = new ProductCategory();
            product.ProductType = new Models.ProductType();
            product.ProductAttributes = new List<ProductAttributes>();

            int.TryParse(form["ImageId"], out var imageId);
            
            var image = new Image(imageId);
            image.ImageName = form["ImageName"];
            image.Path = form["Path"];
            image.Alt = form["Alt"];

            int.TryParse(form["Id"], out var productId);
            int.TryParse(form["ProductCategory.Id"], out var productCategoryId);
            int.TryParse(form["ProductType.Id"], out var productTypeId);
            decimal.TryParse(form["FullPrice"], out var fullPrice);

            product.Id = productId;
            product.Name = form["Name"];
            product.ProductCategory.Id = productCategoryId;
            product.ProductType.Id = productTypeId;
            product.Description = form["Description"];
            product.FullPrice = fullPrice;
            product.Image = image;          

            var formProductAttributes = ExtractProductAttributes(form);
            var productAttributes = new List<ProductAttributes>();

            if (!formProductAttributes.First().Keys.Any()) 
            {
                return product;
            }

            foreach (var singleAttribute in formProductAttributes)
            {
                var productAttribute = new ProductAttributes();

                var id = singleAttribute.FirstOrDefault(sa => sa.Key.Contains("Attribute.Id")).Value;
                productAttribute.Id = int.Parse(id.ToString());

                var gender = singleAttribute.FirstOrDefault(sa => sa.Key.Contains("Gender")).Value;
                productAttribute.Gender = (Gender)Enum.Parse(typeof(Gender), gender.ToString());

                var size = singleAttribute.FirstOrDefault(sa => sa.Key.Contains("Size")).Value;
                productAttribute.Size = (Size)Enum.Parse(typeof(Size), size.ToString());

                var colour = singleAttribute.FirstOrDefault(sa => sa.Key.Contains("Colour")).Value;
                productAttribute.Colour = (Colour)Enum.Parse(typeof(Colour), colour.ToString());

                var stockLevel = singleAttribute.FirstOrDefault(sa => sa.Key.Contains("StockLevel")).Value;
                productAttribute.StockLevel = int.Parse(stockLevel.ToString());

                var priceAdj = singleAttribute.FirstOrDefault(sa => sa.Key.Contains("PriceAdj")).Value;
                productAttribute.PriceAdjustment = decimal.Parse(priceAdj.ToString());

                productAttributes.Add(productAttribute);
            }

            product.ProductAttributes = productAttributes;

            return product;
        }
    }
}