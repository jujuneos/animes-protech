using Animes.Application.DTOs;
using Animes.Application.Interfaces;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces;

namespace Animes.Application.Services;

public class LoginService : ILoginService
{
    private readonly ILoginRepository _loginRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginService(ILoginRepository loginRepository, IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
        _loginRepository = loginRepository;
    }

    public LoginResponse Authenticate(LoginRequest request)
    {
        var user = _loginRepository.GetUserByName(request.Username!);

        if (user == null || !VerifyPassword(request.Senha!, user.Senha!))
        {
            throw new UnauthorizedAccessException("Credenciais inválidas!");
        }

        var token = _jwtTokenService.GetToken(user);

        return new LoginResponse
        {
            Token = token,
            Expiracao = DateTime.UtcNow.AddHours(1)
        };
    }

    private bool VerifyPassword(string senhaInformada, string senhaUsuario)
    {
        if (senhaInformada == senhaUsuario)
            return true;

        return false;
    }

    public void AddUser(Login user)
    {
        _loginRepository.CreateUser(user);
    }
}
