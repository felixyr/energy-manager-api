using EnergyManager.Contracts.IServices;
using EnergyManager.Models.Constants;
using EnergyManager.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnergyManager.Web.Controllers
{
    [Route("api/[controller]")]
    public class ReadingsController: Controller
    {
        private readonly ILogger<ReadingsController> _logger;
        private readonly IReadingService _readingService;

        public ReadingsController(ILogger<ReadingsController> logger, IReadingService readingService)
        {
            _logger = logger;
            _readingService = readingService;
        }

        /// <summary>
        /// Processes an uploaded file containing meter readings and persist the readings.
        /// </summary>
        /// <param name="upload">The file upload containing meter readings.</param>
        /// <returns>An object that contains the number of succeeded and failed meter reading entries.</returns>
        /// <remarks>Accepts a POST request to upload meter readings in a file as part of form data</remarks>
        [HttpPost("meter-reading-uploads")]
        public Statistics Import([FromForm] Upload upload)
        {
            try
            {                
                if (upload == null || upload.File == null || upload.File.Length == 0)
                {
                    _logger.LogInformation("Invalid action parameters, missing or empty file provided");

                    return new Statistics { Failed = 0, Succeeded = 0 };
                }

                // Get the file extension of the uploaded file
                var extension = Path.GetExtension(upload.File.FileName);

                // Check if the file extension is supported
                if (!Constants.SupportedExtensions.Contains(extension))
                {                   
                    _logger.LogInformation($"Unsupported file extension: {extension}");
                    
                    return new Statistics { Failed = 0, Succeeded = 0 };
                }

                _logger.LogInformation($"Processing uploaded file with name {upload.File.FileName}");

                return _readingService.ReadAndStoreReadings(upload);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occurred processing file");
                
                return new Statistics { Failed = 0, Succeeded= 0 };
            }
        }
    }
}
