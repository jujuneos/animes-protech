using Animes.Application.DTOs;
using Animes.Application.Interfaces;
using Animes.Application.Services;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces;
using Moq;
using Xunit;

namespace Animes.Tests;

// Classe para realizar os testes unitários da Autenticação usando Xunit
public class LoginTests
{
    private readonly Mock<ILoginRepository> _loginRepositoryMock;
    private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;
    private readonly LoginService _loginService;

    public LoginTests()
    {
        _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        _loginRepositoryMock = new Mock<ILoginRepository>();
        _loginService = new LoginService(_loginRepositoryMock.Object, _jwtTokenServiceMock.Object);
    }

    // Testa uma autenticação correta
    [Fact]
    public void AutenticacaoValida()
    {
        // Arrange
        var request = new LoginRequest
        {
            Username = "testUser",
            Senha = "senha123"
        };

        var user = new Login
        {
            Username = "testUser",
            Senha = "senha123"
        };

        _loginRepositoryMock.Setup(repo => repo.GetUserByName(request.Username)).Returns(user);
        _jwtTokenServiceMock.Setup(service => service.GetToken(user)).Returns("token-valido");

        // Act
        var response = _loginService.Authenticate(request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("token-valido", response.Token);
        Assert.True(response.Expiracao > DateTime.UtcNow);
    }

    // Testa uma autenticação incorreta
    [Fact]
    public void AutenticacaoInvalida()
    {
        // Arrange
        var request = new LoginRequest
        {
            Username = "invalidUser",
            Senha = "senhaInvalida"
        };

        _loginRepositoryMock.Setup(repo => repo.GetUserByName(request.Username)).Returns((Login)null);

        // Act

        // Assert
        Assert.Throws<UnauthorizedAccessException>(()  => _loginService.Authenticate(request));
    }
}
