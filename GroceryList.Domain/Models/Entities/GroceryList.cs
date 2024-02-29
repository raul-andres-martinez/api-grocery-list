namespace GroceryList.Domain.Models
{
    public class GroceryList : EntityBase
    {
        public GroceryList(List<GroceryListItem> items, Guid createdBy)
        {
            Items = items;
            CreatedBy = createdBy;
        }

        public List<GroceryListItem> Items { get; set; }
        public Guid CreatedBy { get; set; }

        public ICollection<UserGroceryList> Users { get; set; }
    }
}
