using CsvHelper;
using EnergyManager.Contracts.IDatabase;
using EnergyManager.Models.Constants;
using EnergyManager.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;

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
            await SeedTestAccountsAsync();  
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

                var result = await _userManager.CreateAsync(user, "Test1234");

                if (!result.Succeeded)
                {
                    _logger.LogError($"Creating user {user.UserName} failed with error: {result.Errors.Select(e => e.Description).ToArray()}");
                    return;
                }

                _logger.LogInformation("Creation of default user completed successfully");
            }
        }

        private async Task SeedTestAccountsAsync()
        {
            if (!await _context.Accounts.AnyAsync())
            {
                _logger.LogInformation("Seeding test accounts");

                var testAccountsPath = Path.Combine(Environment.CurrentDirectory, Constants.Files, Constants.TestAccounts);

                using var reader = new StreamReader(testAccountsPath);

                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var testAccounts = csv.GetRecords<Models.Models.Account>().ToList();

                _context.Accounts.AddRange(testAccounts.Select(k => new Account
                {
                    AccountId = k.AccountId,
                    FirstName = k.FirstName,
                    LastName = k.LastName
                }));

                await _context.SaveChangesAsync();

                _logger.LogInformation("Seeding test accounts");
            }
        }
    }
}
