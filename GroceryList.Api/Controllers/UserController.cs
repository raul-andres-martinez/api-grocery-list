using GroceryList.Domain.Dtos.Requests;
using GroceryList.Domain.Dtos.Response;
using GroceryList.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroceryList.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUser(UserRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.AddUserAsync(user);

            if (!response.Success) 
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
    }
}
