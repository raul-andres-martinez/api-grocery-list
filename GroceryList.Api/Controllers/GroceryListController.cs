using GroceryList.Domain.Dtos.Requests;
using GroceryList.Domain.Dtos.Response;
using GroceryList.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroceryList.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/grocery-list")]
    public class GroceryListController : ControllerBase
    {
        private readonly IGroceryListService _groceryListService;

        public GroceryListController(IGroceryListService groceryListService)
        {
            _groceryListService = groceryListService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddGroceryList(GroceryListRequest request)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var response = await _groceryListService.AddGroceryListAsync(request);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
    }
}
