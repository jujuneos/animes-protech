using Animes.Application.DTOs;
using Animes.Domain.Entities;

namespace Animes.Application.Interfaces;

public interface ILoginService
{
    LoginResponse Authenticate(LoginRequest request);
    void AddUser(Login user);
}
