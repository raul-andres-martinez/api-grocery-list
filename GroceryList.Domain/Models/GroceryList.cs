namespace GroceryList.Domain.Models
{
    public class GroceryList : EntityBase
    {
        public GroceryList(List<GroceryItem> items, List<Guid> userId)
        {
            Items = items;
            UserId = userId;
        }

        public List<GroceryItem> Items { get; set; }
        public List<Guid> UserId { get; set; }
    }
}
