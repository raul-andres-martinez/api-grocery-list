using GroceryList.Domain.Interfaces.Configs;
using Microsoft.Extensions.Configuration;

namespace GroceryList.Domain.Models.Configs
{
    public class ConfigurationConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConfiguration _configuration;

        public ConfigurationConnectionStringProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString
        {
            get
            {
                var conn = _configuration.GetConnectionString("DefaultConnection");

                if (conn == null)
                {
                    throw new InvalidOperationException("Failed to retrieve ConnectionString from app.settings.");
                }

                return conn;
            }
        }
    }
}
