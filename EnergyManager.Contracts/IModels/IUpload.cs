
using Microsoft.AspNetCore.Http;

namespace EnergyManager.Contracts.IModels
{
    public interface IUpload
    {
        public IFormFile File { get; set; }
    }
}
