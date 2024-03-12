using System.ComponentModel.DataAnnotations;

namespace GroceryList.Domain.Dtos.Requests
{
    public class GroceryListRequest
    {
        public GroceryListRequest(string title)
        {
            Title = title;
        }

        [Required]
        public string Title { get; set; }

        public Models.GroceryList ToEntity(Guid userId)
        {
            return new Models.GroceryList(Title, userId);
        }
    }
}
