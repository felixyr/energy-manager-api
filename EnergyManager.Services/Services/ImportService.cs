using CsvHelper;
using EnergyManager.Contracts.IServices;
using EnergyManager.Models.Models;
using EnergyManager.Services.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace EnergyManager.Services.Services
{
    public class ImportService: IImportService
    {
        private readonly ILogger<ImportService> _logger;
        public ImportService(ILogger<ImportService> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Import meter readings from provided file
        /// </summary>
        /// <param name="uploadStream"></param>
        /// <returns></returns>
        public Import ImportReadingsFromFile(IFormFile formFile)
        {
            var uploadStream = formFile.OpenReadStream();

            using var reader = new StreamReader(uploadStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var import = new Import();

            var processedReadings = new HashSet<string>();

            while (csv.Read())
            {
                try
                {
                    var reading = csv.GetRecord<Reading>();

                    // Validate the record and add it to the list if valid
                    if (ReadingUtility.IsValid(reading, processedReadings))
                    {
                        import.Readings.Add(reading);
                        processedReadings.Add(reading.GenerateCompositeKey());
                    }
                    else
                    {
                        import.Statistics.Failed++;
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError($"Error processing row: {exception.Message}");
                    import.Statistics.Failed++;
                }
            }

            return import;
        }
    }
}
