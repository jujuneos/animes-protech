using Animes.Domain.Entities;

namespace Animes.Domain.Interfaces;

// Interface do repositório de Login
public interface ILoginRepository
{
    Login GetUserByName(string name);
    void CreateUser(Login user);
}
