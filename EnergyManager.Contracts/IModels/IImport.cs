
namespace EnergyManager.Contracts.IModels
{
    public interface IImport
    {
        int Succeeded { get; }        
        int Failed { get; set; }
    }
}
