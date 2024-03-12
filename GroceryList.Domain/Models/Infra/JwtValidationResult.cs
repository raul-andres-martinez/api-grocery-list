using System.Security.Claims;

namespace GroceryList.Domain.Models.Infra
{
    public class JwtValidationResult
    {
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }
        public ClaimsIdentity Claims { get; set; }

        public JwtValidationResult(bool isValid, string? errorMessage, ClaimsIdentity? claims)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
            Claims = claims;
        }
    }
}
