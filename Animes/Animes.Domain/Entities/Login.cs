namespace Animes.Domain.Entities;

/// <summary>
/// Entidade para representar um usuário no projeto.
/// </summary>
public class Login
{
    /// <value>A propriedade <c>Id</c> é a chave primária do usuário.</value>
    public int Id { get; set; }

    /// <value>Propriedade que representa o nome de usuário utilizado no login.</value>
    public string? Username { get; set; }

    /// <value>Propriedade que representa a senha de acesso do usuário.</value>
    public string? Senha { get; set; }
}
