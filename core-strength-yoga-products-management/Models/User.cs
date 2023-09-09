#nullable disable 

namespace core_strength_yoga_products_management.Models
{
    public class User
    {
        public User() 
        {
            Username = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            Roles = new List<string>();        
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
