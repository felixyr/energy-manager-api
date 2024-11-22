using EnergyManager.Contracts.IServices;
using EnergyManager.Contracts.IUnitsOfWork;

namespace EnergyManager.Services.Services
{
    public class AccountService: IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool AccountExists(int accountId)
        {
            return _unitOfWork.AccountRepository.Any(k => k.AccountId == accountId);                                                
        }
    }
}
