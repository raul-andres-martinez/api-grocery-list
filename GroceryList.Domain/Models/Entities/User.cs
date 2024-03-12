using System.Security.Cryptography;
using System.Text;
using GroceryList.Domain.Models.Relationships;

namespace GroceryList.Domain.Models
{
    public class User : EntityBase
    {
        public User() { }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            GroceryLists = new List<UserGroceryList>();
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt {  get; private set; }

        public ICollection<UserGroceryList> GroceryLists { get; set; }

        public bool VerifyPasswordHash(string password)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(PasswordHash);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}