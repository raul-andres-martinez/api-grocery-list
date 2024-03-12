using GroceryList.Domain.Models.Infra;

namespace GroceryList.Domain.Interfaces.Infra
{
    public interface IAuthService
    {
        string GenerateJwtToken(Guid userId);
        Task<JwtValidationResult> ValidateJwtTokenAsync(string jwtToken);
        string EncryptUserId(Guid userId);
        Guid DecryptUserId(string encryptedUserId);
    }
}
