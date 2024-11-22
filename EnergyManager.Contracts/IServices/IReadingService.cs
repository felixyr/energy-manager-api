using EnergyManager.Models.Models;

namespace EnergyManager.Contracts.IServices
{
    public interface IReadingService
    {
        Statistics ReadAndStoreReadings(Upload upload);
    }
}
