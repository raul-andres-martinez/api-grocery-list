using GroceryList.Domain.Dtos.Requests;
using GroceryList.Domain.Dtos.Response;

namespace GroceryList.Domain.Interfaces.Services
{
    public interface IGroceryListService
    {
        Task<GenericResponse<Guid>> AddGroceryListAsync(GroceryListRequest request);
    }
}
