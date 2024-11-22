namespace EnergyManager.Contracts.IDatabase
{
    public interface IDatabaseInitializer
    {
        /// <summary>
        /// Seeds the database with default data
        /// </summary>
        /// <returns></returns>
        Task SeedAsync();
    }
}
