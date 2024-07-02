namespace Animes.Domain.Entities;

// Entidade para autenticação
public class Login
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Senha { get; set; }
}
