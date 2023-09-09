namespace core_strength_yoga_products_management.Interfaces
{
    public interface ITokenService
    {
        string JwtToken { get; set; }
        bool ValidateToken();
        void RevokeToken();
    }
}
