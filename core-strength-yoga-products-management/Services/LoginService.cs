using core_strength_yoga_products_management.Interfaces;
using core_strength_yoga_products_management.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace core_strength_yoga_products_management.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILogger<LoginService> _logger;
        private readonly HttpClient _httpClient;
        public LoginService(ILogger<LoginService> logger, HttpClient httpClient) 
        {
            _logger = logger;
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("http://localhost:5131");
        }
        public string JwtToken { get; private set; } 

        public async Task SaveJwtToken(User user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await _httpClient.PostAsync("/api/v1/auth/login", content);
            string resultContent = await result.Content.ReadAsStringAsync();

            var jsonObject = JObject.Parse(resultContent);
            var tokenValue = jsonObject.GetValue("token").ToString();
            var tokenHandler = new JwtSecurityTokenHandler();

            // Read the JWT token
            var jwtToken = tokenHandler.ReadJwtToken(tokenValue);
            JwtToken = tokenValue;
        }

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
