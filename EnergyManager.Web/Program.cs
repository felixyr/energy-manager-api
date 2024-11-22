using EnergyManager.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using EnergyManager.Web.Extensions;
using EnergyManager.Contracts.IDatabase;
using EnergyManager.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace EnergyManager.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<EnergyManagerContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.UseOpenIddict();
            });

            //Add identity into the pipeline
            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<EnergyManagerContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddCors();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = IdentityConfiguration.ApiFriendlyName, Version = "v1.0" });               
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });

            //Services, managers and repositories.
            builder.Services.ConfigureDependencies();

            // File Logger
            builder.Logging.AddFile(builder.Configuration.GetSection("Logging"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseAuthorization();
            app.MapControllers();

            // Setup CORS 
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            // Setup Swagger API documentation 
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Swagger UI - Energy Manager";
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", $"{IdentityConfiguration.ApiFriendlyName} V1.0");
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.Map("api/{**slug}", context =>
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return Task.CompletedTask;
            });

            app.MapFallbackToFile("index.html");

            // Seed initial database
            await SeedDatabaseAsync(app); 

            await app.RunAsync();
        }

        /// <summary>
        /// This method seeds the databsae with default data
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private static async Task SeedDatabaseAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var databaseInitializer = services.GetRequiredService<IDatabaseInitializer>();

                    await databaseInitializer.SeedAsync();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    logger.LogCritical(ex, "Error whilst creating and seeding database");                   
                }
            }
        }
    }
}
