using EnergyManager.Models.Enums;

namespace EnergyManager.Models.Base
{
    public interface IEntity
    {
        int Id { get; set; }              
        DateTime LastUpdated { get; set; }
        EntityStatus EntityStatus { get; set; }
    }
}
