namespace GroceryList.Domain.Models
{
    public class User : EntityBase
    {
        public User(string name, string email, byte[] passwordHash, byte[] passwordSalt)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            GroceryListId = new List<Guid>();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt {  get; set; }
        public List<Guid> GroceryListId { get; set; }
    }
}
