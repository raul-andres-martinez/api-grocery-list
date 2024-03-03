using GroceryList.Domain.Dtos.Requests;
using GroceryList.Domain.Dtos.Response;
using GroceryList.Domain.Interfaces.Interfaces;
using GroceryList.Domain.Interfaces.Services;

namespace GroceryList.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GenericResponse<Guid>> AddUserAsync(UserRequest request)
        {
            var user = request.ToEntity();
            return await _userRepository.AddUserAsync(user);
        }
    }
}
