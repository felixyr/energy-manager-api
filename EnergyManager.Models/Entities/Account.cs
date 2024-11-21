
using EnergyManager.Models.Base;

namespace EnergyManager.Models.Entities
{
    public class Account : Entity
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
