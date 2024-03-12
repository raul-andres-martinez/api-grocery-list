using GroceryList.Domain.Interfaces.Infra;
using GroceryList.Domain.Models.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace GroceryList.Infra.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly string _encryptionKey;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtTokenConfig = new JwtTokenConfig();
            _configuration.GetSection("Jwt").Bind(_jwtTokenConfig);
            _encryptionKey = _configuration.GetSection("EncryptionKey").Value;
        }

        public string GenerateJwtToken(Guid userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.Key));

            var claims = new Dictionary<string, object>
            {
                ["role"] = "User",
                ["sub"] = EncryptUserId(userId)
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

        public async Task<JwtValidationResult> ValidateJwtTokenAsync(string jwtToken)
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
            return new JwtValidationResult(result.IsValid, result.Exception?.Message, result.ClaimsIdentity);  
        }

        public string EncryptUserId(Guid userId)
        {
            var key = Encoding.UTF8.GetBytes(_encryptionKey);
            var iv = new byte[16];

            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                var encryptor = aes.CreateEncryptor();
                var encryptedUserId = encryptor.TransformFinalBlock(userId.ToByteArray(), 0, userId.ToByteArray().Length);

                return Convert.ToBase64String(iv) + "-" + Convert.ToBase64String(encryptedUserId);
            }
        }

        public Guid DecryptUserId(string encryptedUserId)
        {
            string base64Data = encryptedUserId.Split(':')[1].Trim();

            var key = Encoding.UTF8.GetBytes(_encryptionKey);
            var parts = base64Data.Split('-');

            var iv = Convert.FromBase64String(parts[0]);
            var encryptedBytes = Convert.FromBase64String(parts[1]);

            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                var decryptor = aes.CreateDecryptor();
                var decryptedUserId = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                return new Guid(decryptedUserId);
            }
        }
    }
}
