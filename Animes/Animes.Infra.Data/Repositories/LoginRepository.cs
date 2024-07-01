using Animes.Domain.Entities;
using Animes.Domain.Interfaces;
using Animes.Infra.Data.Context;

namespace Animes.Infra.Data.Repositories;

public class LoginRepository : ILoginRepository
{
    private readonly AnimesDbContext _context;

    public LoginRepository(AnimesDbContext context)
    {
        _context = context;
    }

    public Login GetUserByName(string name)
    {
        return _context.Usuarios.SingleOrDefault(u => u.Username == name);
    }

    // Não foi implementado um sistema de criptografia para a senha
    public void CreateUser(Login user)
    {
        _context.Usuarios.Add(user);
        _context.SaveChanges();
    }
}
