using core_strength_yoga_products_management.Interfaces;

namespace core_strength_yoga_products_management.Services
{
    public class TokenService : ITokenService
    {
        public TokenService() 
        { 
            JwtToken = string.Empty;
        }

        public string JwtToken { get; set; }

        public bool ValidateToken()
        {
            return !string.IsNullOrEmpty(JwtToken);
        }
        public void RevokeToken()
        {
            JwtToken = string.Empty;
        }
    }
}
