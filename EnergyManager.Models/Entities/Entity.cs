using EnergyManager.Contracts.IModels;
using EnergyManager.Enums.Enums;
using System.ComponentModel.DataAnnotations;

namespace EnergyManager.Models.Entities
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {            
            LastUpdated = DateTime.UtcNow;
            EntityStatus = EntityStatus.Active;
        }

        [Key]
        public int Id { get; set; }  
        public DateTime LastUpdated { get; set; }
        public EntityStatus EntityStatus { get; set; }
    }
}
