using GroceryList.Domain.Dtos.Requests;
using GroceryList.Domain.Dtos.Response;
using GroceryList.Domain.Models;

namespace GroceryList.Domain.Interfaces.Interfaces
{
    public interface IUserRepository
    {
        Task<GenericResponse<Guid>> AddUserAsync(User user);
        Task<GenericResponse<User>> SignInAsync(string email);
    }
}