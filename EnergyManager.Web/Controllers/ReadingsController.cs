using Microsoft.AspNetCore.Mvc;

namespace EnergyManager.Web.Controllers
{
    public class ReadingsController: Controller
    {
        private readonly ILogger<ReadingsController> _logger;

        public ReadingsController(ILogger<ReadingsController> logger)
        {
            _logger = logger;
        }
    }
}
