namespace GroceryList.Domain.Models
{
    public class UserGroceryList
    {
        public Guid UserId { get; set; }
        public Guid GroceryListId { get; set; }

        public User User { get; set; }
        public GroceryList GroceryList { get; set; }
    }
}
