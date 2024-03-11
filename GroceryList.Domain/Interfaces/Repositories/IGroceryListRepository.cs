using GroceryList.Domain.Dtos.Response;

namespace GroceryList.Domain.Interfaces.Repositories
{
    public interface IGroceryListRepository
    {
        Task<GenericResponse<Guid>> AddGroceryListAsync(Models.GroceryList groceryList);
    }
}
