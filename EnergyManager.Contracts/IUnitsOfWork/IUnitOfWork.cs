using EnergyManager.Contracts.IRepository;

namespace EnergyManager.Contracts.IUnitsOfWork
{
    /// <summary>
    /// Represents a unit of work pattern that coordinates changes across multiple repositories and commits them as a single transaction.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the repository for managing reading entities.
        /// </summary>
        IReadingRepository ReadingRepository { get; }
        /// <summary>
        /// Gets the repository for managing account entities.
        /// </summary>
        IAccountRepository AccountRepository { get; }
        /// <summary>
        /// Saves all changes made across all repositories in the current unit of work.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
