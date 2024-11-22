using EnergyManager.Contracts.IDatabase;
using EnergyManager.Contracts.IServices;
using EnergyManager.Contracts.IUnitsOfWork;
using EnergyManager.Data.DataContext;
using EnergyManager.Data.UnitsOfWork;
using EnergyManager.Services.Services;

namespace EnergyManager.Web.Extensions
{
    /// <summary>
    /// Utility class containing dependency injection helper methods
    /// </summary>
    public static class Dependencies
    {
        /// <summary>
        /// Extension method to add services to DI container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            // Adding services as scoped lifetime, ensuring a new instance is created for http request. 

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IReadingService, ReadingService>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IImportService, ImportService>();

            // Setting the database initialiser lifetime as transient, to be created for each request everytime regardless
            // This means that it can be re-created even if within the same request context
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

            return services;
        }
    }
}
