namespace GroceryList.Domain.Models
{
    public class GroceryItemCategory : EntityBase
    {
        public GroceryItemCategory(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
