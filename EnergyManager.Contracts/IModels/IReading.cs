
namespace EnergyManager.Contracts.IModels
{
    public interface IReading
    {
        int AccountId { get; set; }
        DateTime MeterReadingDateTime { get; set; }
        int MeterReadValue { get; set; }
    }
}
