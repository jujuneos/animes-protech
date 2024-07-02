using Animes.Domain.Entities;

namespace Animes.Application.Interfaces;

public interface IJwtTokenService
{
    string GetToken(Login user);
}
