using EnergyManager.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EnergyManager.Models.Base
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
