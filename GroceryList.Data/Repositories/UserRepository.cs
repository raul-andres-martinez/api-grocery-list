using Dapper;
using GroceryList.Domain.Dtos.Requests;
using GroceryList.Domain.Dtos.Response;
using GroceryList.Domain.Interfaces.Configs;
using GroceryList.Domain.Interfaces.Interfaces;
using GroceryList.Domain.Models;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using static GroceryList.Domain.Constants.Constant;

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

                    const string sql = Queries.InsertUser;

                    var sqlParams = new DynamicParameters();
                    sqlParams.Add("@Id", user.Id);
                    sqlParams.Add("@Name", user.Name);
                    sqlParams.Add("@Email", user.Email);
                    sqlParams.Add("@PasswordHash", user.PasswordHash);
                    sqlParams.Add("@PasswordSalt", user.PasswordSalt);

                    int rowsAffected = await conn.ExecuteAsync(sql, sqlParams);

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

        public async Task<GenericResponse<User>> SignInAsync(string email)
        {
            try
            {
                using (var conn = new MySqlConnection(_connProvider.ConnectionString))
                {
                    await conn.OpenAsync();

                    const string sql = Queries.SelectLogin;

                    var sqlParams = new DynamicParameters();
                    sqlParams.Add("@Email", email);

                    var user = await conn.QuerySingleOrDefaultAsync<User>(sql, sqlParams);

                    if (user is null)
                    {
                        return new GenericResponse<User>($"Email or password does not match.");
                    }

                    return new GenericResponse<User>($"User found.", user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error SigningIn | Message {Message} | Operation: {MethodName}", ex.Message, nameof(SignInAsync));
                return new GenericResponse<User>($"Error SignIn: {ex.Message}");
            }
        }
    }
}
