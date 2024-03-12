using Moq;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.UserAdmin;
using Teste.Tecnico.Mottu.Matheus.Willock.Application.Usecases.UserAdminUsecases;
using Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Repositories.UserAdminRepository;

namespace Teste.Tecnico.Mottu.Matheus.Willock.UnitTests
{
    public class UserAdminUseCaseTests
    {
        
        private readonly Mock<IUserAdminRepository> _userAdminRepositoryMock;
        private readonly UserAdminUseCase _userAdminUseCase;

        public UserAdminUseCaseTests()
        {
            _userAdminRepositoryMock = new Mock<IUserAdminRepository>();
            _userAdminUseCase = new UserAdminUseCase(_userAdminRepositoryMock.Object);
        }

        [Fact]
        public async Task RegisterUserAdmin_ShouldReturnOutputInvalid_WhenUserIsInvalid()
        {
            // Arrange
            var user = new InputUserAdminToRegisterDto("Test User", "testuser@test.com", "Test@1234");

            // Act
            var result = await _userAdminUseCase.RegisterUserAdmin(user);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task LoginUser_ShouldReturnOutputInvalid_WhenCredentialsAreInvalid()
        {
            // Arrange
            var user = new InputUserAdminDto("testuser@test.com", "Test@1234");

            // Act
            var result = await _userAdminUseCase.LoginUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
        }
        
    }
}