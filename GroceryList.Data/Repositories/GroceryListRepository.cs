using GroceryList.Domain.Dtos.Response;
using GroceryList.Domain.Interfaces.Configs;
using GroceryList.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Text.Json;
using static GroceryList.Domain.Constants.Constant;

namespace GroceryList.Data.Repositories
{
    public class GroceryListRepository : IGroceryListRepository
    {
        private readonly IConnectionStringProvider _connProvider;
        private readonly ILogger<GroceryListRepository> _logger;

        public GroceryListRepository(IConnectionStringProvider connProvider, ILogger<GroceryListRepository> logger)
        {
            _connProvider = connProvider;
            _logger = logger;
        }

        public async Task<GenericResponse<Guid>> AddGroceryListAsync(Domain.Models.GroceryList groceryList)
        {
            try
            {
                _logger.LogInformation("Inserting GroceryList {GroceryListId} for User {UserId} | Operation: {MethodName}", groceryList.Id, groceryList.CreatedBy, nameof(AddGroceryListAsync));
                using (var conn = new MySqlConnection(_connProvider.ConnectionString))
                {
                    await conn.OpenAsync();

                    const string sql = Queries.InsertGroceryList;

                    var command = new MySqlCommand(sql, conn);

                    command.Parameters.AddWithValue("@Id", groceryList.Id);
                    command.Parameters.AddWithValue("@Title", groceryList.Title);
                    command.Parameters.AddWithValue("@CreatedBy", groceryList.CreatedBy);
                    command.Parameters.AddWithValue("@CreatedAt", groceryList.CreatedAt);
                    command.Parameters.AddWithValue("@UpdatedAt", groceryList.UpdatedAt);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 1)
                    {
                        string sqlUserGroceryList = Queries.InsertUserGroceryList;

                        var commandUserGroceryList = new MySqlCommand(sqlUserGroceryList, conn);

                        commandUserGroceryList.Parameters.AddWithValue("@Id", Guid.NewGuid());
                        commandUserGroceryList.Parameters.AddWithValue("@UserId", groceryList.CreatedBy);
                        commandUserGroceryList.Parameters.AddWithValue("@GroceryListId", groceryList.Id);

                        await commandUserGroceryList.ExecuteNonQueryAsync();

                        _logger.LogInformation("GroceryList created {GroceryListId} | Operation: {MethodName}", groceryList.Id, nameof(AddGroceryListAsync));
                        return new GenericResponse<Guid>("Grocery list created.", groceryList.Id);
                    }

                    _logger.LogWarning("Error adding GroceryList: {GroceryList} | Operation: {MethodName}", JsonSerializer.Serialize(groceryList), nameof(AddGroceryListAsync));
                    return new GenericResponse<Guid>("Error adding grocery list. No rows changed.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding GroceryList: {GroceryList} | Message {Message} | Operation: {MethodName}", JsonSerializer.Serialize(groceryList), ex.Message, nameof(AddGroceryListAsync));
                return new GenericResponse<Guid>($"Error adding grocery list: {ex.Message}");
            }
        }
    }
}
