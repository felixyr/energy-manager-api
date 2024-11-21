using EnergyManager.Contracts.IRepository;
using EnergyManager.Data.DataContext;
using EnergyManager.Models.Entities;

namespace EnergyManager.Data.Repositories
{
    public class AccountRepository(EnergyManagerContext context) : Repository<Account>(context), IAccountRepository
    {
    }
}
