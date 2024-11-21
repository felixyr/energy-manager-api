using EnergyManager.Contracts.IServices;
using EnergyManager.Models.Models;
using CsvHelper;
using System.Globalization;
using EnergyManager.Services.Utilities;

namespace EnergyManager.Services.Services
{
    public class ReadingService : IReadingService
    {
        public async Task<Import> ImportFromFile(Upload upload)
        {
            var uploadStream = upload.File.OpenReadStream();

            using var reader = new StreamReader(uploadStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            // A list to store valid readings
            var readings = new List<Reading>();

            int invalidRowCount = 0;

            var processedReadings = new List<string>();

            while (csv.Read())
            {
                try
                {
                    var reading = csv.GetRecord<Reading>();

                    // Validate the record and add it to the list if valid
                    if (ReadingUtility.IsValid(reading, processedReadings))
                    {
                        readings.Add(reading);

                        // Mark this reading as processed using the composite key
                        processedReadings.Add(reading.GenerateCompositeKey());
                    }
                    else
                    {
                        invalidRowCount++;
                    }
                }
                catch (Exception exception)
                {
                    // Log an error for rows that cannot be processed
                    Console.WriteLine($"Error processing row: {exception.Message}");
                    invalidRowCount++;
                }
            }

            // Store the valid readings in the database 

            foreach (var reading in readings) { 
              
            
            }

            return new Import { Failed = invalidRowCount, Succeeded = readings.Count };
        }
    }
}
