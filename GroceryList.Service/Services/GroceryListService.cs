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

        public async Task<GenericResponse<Guid>> AddGroceryListAsync(GroceryListRequest request)
        {
            var groceryList = request.ToEntity();
            return await _groceryListRepository.AddGroceryListAsync(groceryList);
        }
    }
}