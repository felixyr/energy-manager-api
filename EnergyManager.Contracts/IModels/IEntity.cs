using EnergyManager.Enums.Enums;

namespace EnergyManager.Contracts.IModels
{
    public interface IEntity
    {
        int Id { get; set; }              
        DateTime LastUpdated { get; set; }
        EntityStatus EntityStatus { get; set; }
    }
}
