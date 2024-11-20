namespace EnergyManager.Contracts.IDatabase
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
}
