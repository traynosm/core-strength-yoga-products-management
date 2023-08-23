using core_strength_yoga_products_management.Interfaces;
using core_strength_yoga_products_management.Models;
using core_strength_yoga_products_management.Models.Enums;
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
        public ProductController(IProductService productService, IWebHostEnvironment hostingEnvironment)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var categories = await _productService.GetCategories();
            ViewData["categories"] = categories;

            var types = await _productService.GetTypes();
            ViewData["types"] = types;

            var products = await _productService.GetProducts();
            return View(products);
        }

        public async Task<IActionResult> GetCategory()
        {
            var categories = await _productService.GetCategories();
            return View(categories);
        }
        public async Task<IActionResult> GetTypes()
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
        public async Task<IActionResult> GetByType(int productTypeId)
        {
            var productsByType = await _productService.GetByProductType(productTypeId);
            if (productsByType == null || !productsByType.Any())
            {
                return RedirectToAction("Index");
            }
            var Types = await _productService.GetTypes();
            ViewData["categories"] = Types;

            return View("Index", productsByType);
        }
        public async Task<IActionResult> Add()
        {
            var selectListItemsCategories = await BuildSelectItemsCategories(0);
            ViewData["selectListItemsCategories"] = selectListItemsCategories;

            var selectListItemsTypes = await BuildSelectItemsTypes(0);
            ViewData["selectListItemsTypes"] = selectListItemsTypes;

            return View(new Product());
        }

        [HttpGet("Product/ProductAttributePartialView")]
        public IActionResult ProductAttributePartialView()
        {
            return PartialView("ProductAttribute", new ProductAttributes());
        }

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
        public async Task<IActionResult> AddOrUpdate(IFormCollection form)
        {
            var product = BuildProductFromFormCollection(form);
            var updatedProduct = new Product();
            //update in db backend
            if (product.Id == 0)
            {
                updatedProduct = await _productService.Add(product);
            }
            else
            {
                updatedProduct = await _productService.Update(product);
            }


            var selectListItemsCategories = await BuildSelectItemsCategories(product.ProductCategory.Id);
            ViewData["selectListItemsCategories"] = selectListItemsCategories;

            var selectListItemsTypes = await BuildSelectItemsTypes(product.ProductType.Id);
            ViewData["selectListItemsTypes"] = selectListItemsTypes;

            return RedirectToAction("Edit", new { updatedProduct.Id });
        }
        [HttpPost]
        public async Task UploadImage(IFormFile image)
        {
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");

                if (image.Length > 0)
                {
                    string filePath = Path.Combine(uploads, image.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                }
            

            return;
            //var body = await HttpContext.Request.Body.ReadAsync(new byte[] { });
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



            product.Id = int.Parse(form["Id"]);
            product.Name = form["Name"];
            product.ProductCategory.Id = int.Parse(form["ProductCategory.Id"]);
            product.ProductType.Id = int.Parse(form["ProductType.Id"]);
            product.Description = form["Description"];
            product.Image = image;
            

            var formProductAttributes = ExtractProductAttributes(form);
            var productAttributes = new List<ProductAttributes>();

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

    public class ImageForm
    {
        public IFormFile[] Files { get; set; }
    }
}