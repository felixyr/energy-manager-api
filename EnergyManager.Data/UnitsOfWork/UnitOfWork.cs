using EnergyManager.Contracts.IRepository;
using EnergyManager.Contracts.IUnitsOfWork;
using EnergyManager.Data.DataContext;
using EnergyManager.Data.Repositories;

namespace EnergyManager.Data.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EnergyManagerContext _context;

        public UnitOfWork(EnergyManagerContext context)
        {
            _context = context;
        }

        private IReadingRepository _readingRepository;
        public IReadingRepository ReadingRepository
        {
            get { return _readingRepository ??= new ReadingRepository(_context); }
        }

        private IAccountRepository _accountRepository;
        public IAccountRepository AccountRepository
        {
            get { return _accountRepository ??= new AccountRepository(_context); }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
