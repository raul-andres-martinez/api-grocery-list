using GroceryList.Domain.Dtos.Requests;
using GroceryList.Domain.Dtos.Response;
using GroceryList.Domain.Interfaces.Infra;
using GroceryList.Domain.Interfaces.Interfaces;
using GroceryList.Domain.Interfaces.Services;

namespace GroceryList.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<GenericResponse<Guid>> AddUserAsync(UserRequest request)
        {
            var user = request.ToEntity();
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<GenericResponse<SignInResponse>> SignInAsync(SignInRequest request)
        {
            var user = await _userRepository.SignInAsync(request.Email);

            if(user.Data is null || !user.Success) 
            {
                return new GenericResponse<SignInResponse>(user.Message);
            }

            var passwordMatch = user.Data.VerifyPasswordHash(request.Password);

            if (!passwordMatch)
            {
                return new GenericResponse<SignInResponse>("Email or password does not match.");
            }

            var token = _authService.GenerateJwtToken(user.Data.Id);
            return new GenericResponse<SignInResponse>("SignIn sucess.", new SignInResponse(token));
        }
    }
}
