using EnergyManager.Models.Entities;

namespace EnergyManager.Contracts.IRepository
{
    /// <summary>
    /// Interface for interacting with meter readings data in the repository pattern.
    /// </summary>
    public interface IReadingRepository : IRepository<Reading>
    {
    }
}
