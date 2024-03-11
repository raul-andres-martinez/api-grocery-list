namespace GroceryList.Domain.Models.Relationships
{
    public class UserGroceryList : EntityBase
    {
        public Guid UserId { get; set; }
        public Guid GroceryListId { get; set; }

        public User User { get; set; }
        public GroceryList GroceryList { get; set; }
    }
}
