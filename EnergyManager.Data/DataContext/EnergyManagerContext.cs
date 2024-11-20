using EnergyManager.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnergyManager.Data.DataContext
{
    public class EnergyManagerContext : IdentityDbContext<User, Role, string>
    {
        public EnergyManagerContext(DbContextOptions options) : base(options)
        { } 

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Reading> Readings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Other mappings
        }
    }
}
