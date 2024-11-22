using EnergyManager.Models.Entities;

namespace EnergyManager.Contracts.IRepository
{
    /// <summary>
    /// Interface for interacting with account data in the repository pattern.
    /// </summary>
    public interface IAccountRepository : IRepository<Account>
    {
    }
}
