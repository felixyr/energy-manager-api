
using EnergyManager.Contracts.IRepository;
using EnergyManager.Contracts.IUnitsOfWork;
using EnergyManager.Models.Entities;
using EnergyManager.Services.Services;
using Moq;
using Xunit;

namespace EnergyManager.Tests.ServiceTests
{
    public class AccountServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockAccountRepository = new Mock<IAccountRepository>();

            // Setup mock to return AccountRepository
            _mockUnitOfWork.Setup(u => u.AccountRepository).Returns(_mockAccountRepository.Object);

            // Instantiate the service with the mocked UnitOfWork
            _accountService = new AccountService(_mockUnitOfWork.Object);
        }

        [Fact]
        public void TestAccountExists()
        {
            // Arrange
            var accountId = 123;
            var accounts = new List<Account>() { new Account { Id = 1, AccountId = accountId, FirstName = "Felix", LastName = "Too" } };

            // Mock the Get method
            _mockAccountRepository.Setup(k => k.Any(x => x.AccountId == accountId)).Returns(accounts.Any(k => k.AccountId == accountId));

            // Act
            var result = _accountService.AccountExists(accountId);

            // Assert
            Assert.True(result);
        }
    }
}

