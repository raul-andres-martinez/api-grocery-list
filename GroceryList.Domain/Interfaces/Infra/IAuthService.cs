using GroceryList.Domain.Models.Infra;

namespace GroceryList.Domain.Interfaces.Infra
{
    public interface IAuthService
    {
        string GenerateJwtToken();
        Task<JwtValidationResult> ValidateJwtToken(string jwtToken);
    }
}
