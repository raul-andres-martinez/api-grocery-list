namespace GroceryList.Domain.Dtos.Response
{
    public class SignInResponse
    {
        public SignInResponse(string token)
        {
            Token = token;
        }
        public string Token { get; set; }

    }
}
