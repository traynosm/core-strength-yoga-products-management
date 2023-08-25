using core_strength_yoga_products_management.Models;

namespace core_strength_yoga_products_management.Interfaces
{
    public interface ILoginService
    {
        string JwtToken { get;}

        Task SaveJwtToken(User user);
        bool ValidateToken();
        void RevokeToken();

    }
}
