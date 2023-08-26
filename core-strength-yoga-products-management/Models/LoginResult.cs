using core_strength_yoga_products_management.Areas.Identity.Data;
#nullable disable

namespace core_strength_yoga_products_management.Models
{
    public class LoginResult
    {
        public ManagementUser IdentityUser { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
