namespace Animes.Domain.Entities;

// Entidade principal do projeto
public class Anime
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Resumo { get; set; }
    public string? Diretor { get; set; }

    // Flag para exclusão lógica
    public bool? Deletado { get; set; }
}

