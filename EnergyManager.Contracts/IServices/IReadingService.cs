using EnergyManager.Models.Models;

namespace EnergyManager.Contracts.IServices
{
    public interface IReadingService
    {
        /// <summary>
        /// Import readings from supplied file
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        Task<Import> ImportFromFile(Upload upload);
    }
}
