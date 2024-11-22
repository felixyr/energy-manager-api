using EnergyManager.Contracts.IServices;
using EnergyManager.Models.Models;
using EnergyManager.Contracts.IUnitsOfWork;
using ReadingEntity = EnergyManager.Models.Entities.Reading;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using CsvHelper;

namespace EnergyManager.Services.Services
{
    public class ReadingService : IReadingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ReadingService> _logger;
        private readonly IImportService _importService;
        private readonly IAccountService _accountService;
        public ReadingService(IImportService importService, IUnitOfWork unitOfWork, IAccountService accountService, ILogger<ReadingService> logger)
        {
            _importService = importService;
            _accountService = accountService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Statistics ReadAndStoreReadings(Upload upload)
        {
            var import = GetReadings(upload.File);

            // TODO: Setup AutoMapper to translate from model to entity
            var readings = import.Readings.Select(k => new ReadingEntity
            {
                AccountId = k.AccountId,
                // It's a requirement to store date in UTC in Postgres
                DateTime = k.MeterReadingDateTime.ToUniversalTime(),
                Value = k.MeterReadValue
            });

            // Commit the valid readings in the database
            BulkInsert(readings);

            import.Statistics.Succeeded = readings.Count();

            return import.Statistics;  
        }

        /// <summary>
        /// Processes an uploaded file to extract and validate meter readings.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        private Import GetReadings(IFormFile formFile)
        {
            // Parse readings from the uploaded file
            var import = _importService.ImportReadingsFromFile(formFile);

            var readings = new List<Reading>();

            foreach (var reading in import.Readings)
            {
                // Check if there is an account with reading account ID
                if (!_accountService.AccountExists(reading.AccountId))
                {
                    _logger.LogInformation($"Skipping reading with account ID {reading.AccountId} as account doesn't exist).");
                    import.Statistics.Failed++;
                    continue;
                }

                var existingReading = GetLatestReadByAccountId(reading.AccountId);

                var readingDateTimeUTC = reading.MeterReadingDateTime.ToUniversalTime();

                // Check if the reading is already in the database based on exact match of date and value
                if (existingReading != null && existingReading.DateTime == readingDateTimeUTC && existingReading.Value == reading.MeterReadValue)
                {
                    _logger.LogInformation($"Skipping duplicate reading with account ID {reading.AccountId}.");
                    import.Statistics.Failed++;
                    continue;
                }

                // Ensure that the reading is newer or equal in date to the latest existing reading
                if (existingReading == null || reading.MeterReadingDateTime >= existingReading.DateTime)
                {
                    readings.Add(reading);
                }
                else {
                    _logger.LogInformation($"Skipping reading with account ID {reading.AccountId} as it is older than existing reading.");
                    import.Statistics.Failed++;
                }                    
            }

            import.Readings = readings;
           
            return import;
        }

        /// <summary>
        /// Query the database to get the most recent reading with provided accountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private ReadingEntity? GetLatestReadByAccountId(int accountId)
        {
            return _unitOfWork.ReadingRepository.Get(k => k.AccountId == accountId)
                                                .OrderByDescending(k => k.DateTime)
                                                .FirstOrDefault();
        }

        public int BulkInsert(IEnumerable<ReadingEntity> readings)
        {
            _unitOfWork.ReadingRepository.AddRange(readings);

            return _unitOfWork.SaveChanges();
        }
    }
}
