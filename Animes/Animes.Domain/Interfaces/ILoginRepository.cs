using Animes.Domain.Entities;

namespace Animes.Domain.Interfaces;

/// <summary>
/// Interface do repositório de Login.
/// </summary>
public interface ILoginRepository
{
    /// <summary>
    /// Método para obter um usuário cadastrado pelo <paramref name="name"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>O usuário cadastrado com <paramref name="name"/></returns>
    Login GetUserByName(string name);

    /// <summary>
    /// Método para criar um usuário por meio dos atributos de <paramref name="user"/>.
    /// </summary>
    /// <param name="user"></param>
    void CreateUser(Login user);
}
