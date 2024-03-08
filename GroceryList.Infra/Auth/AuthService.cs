using GroceryList.Domain.Interfaces.Infra;
using GroceryList.Domain.Models.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace GroceryList.Infra.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtTokenConfig _jwtTokenConfig;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtTokenConfig = new JwtTokenConfig();
            _configuration.GetSection("Jwt").Bind(_jwtTokenConfig);
        }

        public string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Key));

            var claims = new Dictionary<string, object>
            {
                [ClaimTypes.Role] = "User",
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtTokenConfig.Issuer,
                Audience = _jwtTokenConfig.Audience,
                Claims = claims,
                IssuedAt = null,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(180),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new JsonWebTokenHandler();
            handler.SetDefaultTimesOnTokenCreation = false;
            return handler.CreateToken(descriptor);
        }

        public async Task<JwtValidationResult> ValidateJwtToken(string jwtToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Key)),
                ValidateIssuer = true,
                ValidIssuer = _jwtTokenConfig.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtTokenConfig.Audience,
                ValidateLifetime = true,
            };

            var handler = new JsonWebTokenHandler();
            var result = await handler.ValidateTokenAsync(jwtToken, tokenValidationParameters);
            return new JwtValidationResult(result.IsValid, result.Exception.Message);  
        }
    }
}
