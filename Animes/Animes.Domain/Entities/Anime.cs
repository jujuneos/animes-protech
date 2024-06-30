namespace Animes.Domain.Entities;

// Entidade principal do projeto
public class Anime
{
    public int Id { get; private set; }
    public string? Nome { get; private set; }
    public string? Resumo { get; private set; }
    public string? Diretor { get; private set; }

    // Flag para exclusão lógica
    public bool? Deletado { get; set; }
}

