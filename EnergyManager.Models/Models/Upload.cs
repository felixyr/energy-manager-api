using Microsoft.AspNetCore.Http;

namespace EnergyManager.Models.Models
{
    public class Upload
    {
        public IFormFile File { get; set; }
    }
}
