using core_strength_yoga_products_management.Areas.Identity.Data;
using core_strength_yoga_products_management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace core_strength_yoga_products_management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<ManagementUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, 
            SignInManager<ManagementUser> signInManager)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var claim = _httpContextAccessor.HttpContext?.User;

            if (claim != null)
            {
                if (_signInManager.IsSignedIn(_httpContextAccessor!.HttpContext!.User))
                {
                    return RedirectToAction("Welcome");
                }
            }

            return Redirect("~/Identity/Account/Login");
        }

        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}