using EnergyManager.Models.Models;

namespace EnergyManager.Services.Utilities
{
    public static class ReadingUtility
    {
        /// <summary>
        /// Generates a composite key for a meter reading using its properties.
        /// </summary>
        /// <param name="reading">The reading to generate the composite key for.</param>
        /// <returns>A composite key string representing the reading.</returns>
        public static string GenerateCompositeKey(this Reading reading)
        {
            // Convert the date and time to UTC for uniformity 
            var meterReadingDateTime = reading.MeterReadingDateTime.ToUniversalTime();

            return $"{reading.AccountId}-{reading.MeterReadValue}-{meterReadingDateTime:yyyy-MM-dd HH:mm}";
        }

        /// <summary>
        /// Validates a meter reading against provided rules.
        /// </summary>
        /// <param name="reading">The record to validate.</param>
        /// <param name="processedEntries">A list of already processed entries.</param>
        /// <returns>true if the record is valid, otherwise false.</returns>
        public static bool IsValid(this Reading reading, HashSet<string> processedEntries)
        {
            // Ensure the Account ID is present and valid
            if (reading.AccountId <= 0) return false;

            // Ensure meter reading value is in the NNNNN format (5 digits)
            if (reading.MeterReadValue < 10000 || reading.MeterReadValue > 99999) return false;

            var entryKey = reading.GenerateCompositeKey();

            if (processedEntries.Contains(entryKey)) return false;

            return true;
        }
    }
}
