using core_strength_yoga_products_management.Models;

namespace core_strength_yoga_products_management.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResult> Login(User user);

        Task<LoginResult> Register(User user);
    }
}
