using GroceryList.Domain.Models.Relationships;

namespace GroceryList.Domain.Models
{
    public class GroceryList : EntityBase
    {
        public GroceryList(string title, Guid createdBy)
        {
            Title = title;
            Items = new List<GroceryListItem>();
            CreatedBy = createdBy;
            Users = new List<UserGroceryList>();
        }

        public string Title { get; set; }
        public List<GroceryListItem> Items { get; set; }
        public Guid CreatedBy { get; set; }

        public ICollection<UserGroceryList> Users { get; set; }
    }
}
