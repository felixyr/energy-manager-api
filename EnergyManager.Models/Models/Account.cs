using EnergyManager.Contracts.IModels;

namespace EnergyManager.Models.Models
{
    public class Account: IAccount
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
