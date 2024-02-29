using Dapper;
using GroceryList.Domain.Interfaces.Configs;
using MySqlConnector;

namespace GroceryList.Data.Helpers
{
    public class DapperHelper
    {
        private readonly IConnectionStringProvider _connProvider;
        public DapperHelper(IConnectionStringProvider connProvider)
        {
            _connProvider = connProvider;
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object param)
        {
            using (var connection = new MySqlConnection(_connProvider.ConnectionString))
            {
                return await connection.QuerySingleAsync<T>(sql, param);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param)
        {
            using (var connection = new MySqlConnection(_connProvider.ConnectionString))
            {
                return await connection.QueryAsync<T>(sql, param);
            }
        }

        public async Task ExecuteAsync(string sql, object param)
        {
            using (var connection = new MySqlConnection(_connProvider.ConnectionString))
            {
                await connection.ExecuteAsync(sql, param);
            }
        }
    }
}
