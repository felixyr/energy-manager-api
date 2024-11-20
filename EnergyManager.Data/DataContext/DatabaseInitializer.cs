using EnergyManager.Contracts.IDatabase;
using EnergyManager.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace EnergyManager.Data.DataContext
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly EnergyManagerContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;
        public DatabaseInitializer(EnergyManagerContext context, ILogger<DatabaseInitializer> logger, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }
        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();
            await SeedDefaultUserAsync();
        }

        private async Task SeedDefaultUserAsync()
        {
            if (!await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Creating default user");

                var user = new User
                {
                    UserName = "admin",
                    Email = "admin@energymanager.com",
                    Name = "Jane Doe"
                };

                var result = await _userManager.CreateAsync(user, "test1234");

                if (!result.Succeeded)
                {
                    _logger.LogError($"Creating user {user.UserName} failed with error: {result.Errors.Select(e => e.Description).ToArray()}");
                    return;
                }

                _logger.LogInformation("Creation of default user completed successfully");
            }
        }
    }
}
