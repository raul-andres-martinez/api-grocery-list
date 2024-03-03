using GroceryList.Data.Repositories;
using GroceryList.Domain.Interfaces.Configs;
using GroceryList.Domain.Interfaces.Interfaces;
using GroceryList.Domain.Interfaces.Services;
using GroceryList.Domain.Models.Configs;
using GroceryList.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace GroceryList.Service.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Repositories
            services.AddSingleton<IUserRepository, UserRepository>();

            //Services
            services.AddSingleton<IUserService, UserService>();

            // Singleton ConnString provider
            services.AddSingleton<IConnectionStringProvider, ConfigurationConnectionStringProvider>();

            // Serilog config
            var logFilePath = configuration["LogFilePath"];
            if (string.IsNullOrEmpty(logFilePath))
            {
                throw new Exception("LogFilePath is not configured in appsettings.json");
            }
            var loggerConfiguration = ConfigureLogger(logFilePath);
            Log.Logger = loggerConfiguration.CreateLogger();
            services.AddSingleton(Log.Logger);

            return services;
        }

        public static LoggerConfiguration ConfigureLogger(string logFilePath)
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
                .WriteTo.File(
                    logFilePath,
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                );
        }
    }
}