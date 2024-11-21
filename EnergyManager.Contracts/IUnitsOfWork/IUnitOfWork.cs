using EnergyManager.Contracts.IRepository;

namespace EnergyManager.Contracts.IUnitsOfWork
{
    public interface IUnitOfWork
    {
        IReadingRepository ReadingRepository { get; }
        IAccountRepository AccountRepository { get; }
        int SaveChanges();
    }
}
