using GroceryList.Domain.Dtos.Response;
using GroceryList.Domain.Models;

namespace GroceryList.Domain.Interfaces.Interfaces
{
    public interface IUserRepository
    {
        Task<GenericResponse> AddUserAsync(User user);
    }
}
