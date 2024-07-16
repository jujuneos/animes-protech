namespace Animes.Domain.Entities;

/// <summary>
/// Entidade usada para representar um Anime no projeto.
/// </summary>
public class Anime
{
    /// <value>A propriedade <c>Id</c> é usada para representar a chave primária do anime.</value>
    public int Id { get; set; }

    /// <value>Propriedade que representa o nome do anime.</value>
    public string? Nome { get; set; }

    /// <value>Propriedade que representa um breve resumo do anime.</value>
    public string? Resumo { get; set; }

    /// <value>Propriedade que representa o diretor do anime.</value>
    public string? Diretor { get; set; }

    /// <value>A propriedade <c>Deletado</c> é uma flag usada na remoção lógica do anime.</value>
    public bool? Deletado { get; set; }
}

