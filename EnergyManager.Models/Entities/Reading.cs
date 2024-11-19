
namespace EnergyManager.Models.Entities
{
    public class Reading: Entity
    {
        public int AccountId { get; set; }
        public DateTime DateTime { get; set; }
        public int Value { get; set; }
    }
}
