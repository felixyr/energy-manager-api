using EnergyManager.Contracts.IModels;

namespace EnergyManager.Contracts.IServices
{
    public interface IReadingService
    {
        /// <summary>
        /// Import readings from supplied file
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        Task<IImport> ImportFromFile(IUpload upload);
    }
}
