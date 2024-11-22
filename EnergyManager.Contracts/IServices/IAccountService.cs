
namespace EnergyManager.Contracts.IServices
{
    public interface IAccountService
    {
        /// <summary>
        /// Checks if an account with the specified ID exists in the system.
        /// </summary>
        /// <param name="accountId">Account identifier to check</param>
        /// <returns></returns>
        bool AccountExists(int accountId);
    }
}
