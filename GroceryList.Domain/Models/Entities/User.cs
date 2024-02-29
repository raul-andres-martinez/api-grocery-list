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
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt {  get; private set; }

        public ICollection<UserGroceryList> GroceryLists { get; set; }
    }
}