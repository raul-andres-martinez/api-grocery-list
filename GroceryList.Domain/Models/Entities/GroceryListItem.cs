namespace GroceryList.Domain.Models
{
    public class GroceryListItem : EntityBase
    {
        public GroceryListItem(string name, int quantity, List<GroceryItemCategory> category, bool isChecked)
        {
            Name = name;
            Quantity = quantity;
            Category = category;
            IsChecked = isChecked;
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public List<GroceryItemCategory> Category { get; set; }
        public bool IsChecked { get; set; }
    }
}
