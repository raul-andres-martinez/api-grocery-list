namespace GroceryList.Domain.Models.Infra
{
    public class JwtValidationResult
    {
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }

        public JwtValidationResult(bool isValid, string? errorMessage)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }
    }
}
