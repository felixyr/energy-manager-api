using EnergyManager.Models.Models;

namespace EnergyManager.Contracts.IServices
{
    public interface IReadingService
    {
        /// <summary>
        /// Reads meter readings from the provided file and stores them in the database.
        /// </summary>
        /// <param name="upload">The uploaded meter readings file.</param>
        /// <returns></returns>
        Statistics ReadAndStoreReadings(Upload upload);
    }
}
