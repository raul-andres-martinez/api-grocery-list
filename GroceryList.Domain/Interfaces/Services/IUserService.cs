using GroceryList.Domain.Dtos.Requests;
using GroceryList.Domain.Dtos.Response;

namespace GroceryList.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<GenericResponse<Guid>> AddUserAsync(UserRequest request);
    }
}
