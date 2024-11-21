using EnergyManager.Contracts.IServices;
using EnergyManager.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;

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
        /// Imports meter readings from provided file
        /// </summary>
        /// <param name="import">The meter readings file.</param>
        /// <returns>Number of successful and failed readings.</returns>
        [HttpPost("meter-reading-uploads")]
        public async Task<Import> Import([FromForm] Upload upload)
        {
            try
            {
                if (upload == null || upload.File == null || upload.File.Length == 0)
                {
                    _logger.LogInformation("Invalid action parameters, missing or empty file provided");

                    return new Import { Failed = 0, Succeeded = 0 };
                }

                _logger.LogInformation($"Processing uploaded file with name {upload.File.FileName}");

                return await _readingService.ImportFromFile(upload);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occurred processing file");
                
                return new Import { Failed = 0, Succeeded= 0 };
            }
        }
    }
}
