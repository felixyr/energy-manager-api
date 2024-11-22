
namespace EnergyManager.Models.Models
{
    public class Import
    {
        public Statistics Statistics { get; set; } = new();
        public List<Reading> Readings { get; set; } = new();
    }
}
