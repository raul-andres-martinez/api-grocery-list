using GroceryList.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace GroceryList.Domain.Dtos.Requests
{
    public class UserRequest
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public User ToEntity(byte[] passwordHash, byte[] passwordSalt) 
        {
            return new User(Name, Email, passwordHash, passwordSalt);
        }
    }
}
