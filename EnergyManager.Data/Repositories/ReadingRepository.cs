using EnergyManager.Contracts.IRepository;
using EnergyManager.Data.DataContext;
using EnergyManager.Models.Entities;

namespace EnergyManager.Data.Repositories
{
    public class ReadingRepository(EnergyManagerContext context) : Repository<Reading>(context), IReadingRepository
    {
    }
}
