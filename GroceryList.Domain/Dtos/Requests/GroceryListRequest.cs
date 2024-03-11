using System.ComponentModel.DataAnnotations;

namespace GroceryList.Domain.Dtos.Requests
{
    public class GroceryListRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public Models.GroceryList ToEntity()
        {
            return new Models.GroceryList(Title, UserId);
        }
    }
}
