using EnergyManager.Contracts.IModels;

namespace EnergyManager.Models.Models
{
    public class Reading: IReading
    {
        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public int MeterReadValue { get; set; }
    }
}
