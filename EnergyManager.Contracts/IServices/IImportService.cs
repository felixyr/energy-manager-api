using EnergyManager.Models.Models;
using Microsoft.AspNetCore.Http;

namespace EnergyManager.Contracts.IServices
{
    public interface IImportService
    {
        /// <summary>
        /// Import meter readings from provided file
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Import ImportReadingsFromFile(IFormFile formFile);
    }
}
