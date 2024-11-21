using EnergyManager.Contracts.IModels;

namespace EnergyManager.Models.Models
{
    public class Import: IImport
    {
        public int Succeeded { get; set; }
        public int Failed { get; set; }
    }
}
