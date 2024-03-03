using GroceryList.Domain.Dtos.Response;
using GroceryList.Domain.Interfaces.Configs;
using GroceryList.Domain.Interfaces.Interfaces;
using GroceryList.Domain.Models;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace GroceryList.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionStringProvider _connProvider;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IConnectionStringProvider connProvider, ILogger<UserRepository> logger)
        {
            _connProvider = connProvider;
            _logger = logger;
        }

        public async Task<GenericResponse<Guid>> AddUserAsync(User user)
        {
            try
            {
                _logger.LogInformation("Inserting User {UserId} | Operation: {MethodName}", user.Id, nameof(AddUserAsync));
                using (var conn = new MySqlConnection(_connProvider.ConnectionString))
                {
                    await conn.OpenAsync();

                    const string sql = @"
                        INSERT INTO Users (Id, Name, Email, PasswordHash, PasswordSalt, CreatedAt, UpdatedAt)
                        VALUES (@Id, @Name, @Email, @PasswordHash, @PasswordSalt, NOW(), NOW());
                    ";

                    var command = new MySqlCommand(sql, conn);

                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    command.Parameters.AddWithValue("@PasswordSalt", user.PasswordSalt);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 1)
                    {
                        _logger.LogInformation("User created {UserId} | Operation: {MethodName}", user.Id, nameof(AddUserAsync));
                        return new GenericResponse<Guid>("User created.", user.Id);
                    }

                    _logger.LogWarning("Error adding User: {User} | Unique constraint violation Email or Id | Operation: {MethodName}", new { user.Id, user.Email }, nameof(AddUserAsync));
                    return new GenericResponse<Guid>("Error adding user. No rows changed.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding User: {UserId} | Message {Message} | Operation: {MethodName}", user.Id, ex.Message, nameof(AddUserAsync));
                return new GenericResponse<Guid>($"Error adding user: {ex.Message}");
            }
        }
    }
}
