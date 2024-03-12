using GroceryList.Domain.Dtos.Requests;
using GroceryList.Domain.Dtos.Response;
using GroceryList.Domain.Interfaces.Repositories;
using GroceryList.Domain.Interfaces.Services;

namespace GroceryList.Service.Services
{
    public class GroceryListService : IGroceryListService
    {
        private readonly IGroceryListRepository _groceryListRepository;

        public GroceryListService(IGroceryListRepository groceryListRepository)
        {
            _groceryListRepository = groceryListRepository;
        }

        public async Task<GenericResponse<Guid>> AddGroceryListAsync(GroceryListRequest request, string userId)
        {
            Guid.TryParse(userId, out Guid id);
            var groceryList = request.ToEntity(id);
            return await _groceryListRepository.AddGroceryListAsync(groceryList);
        }
    }
}