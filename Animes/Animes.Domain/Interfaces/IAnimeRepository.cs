using Animes.Domain.Entities;

namespace Animes.Domain.Interfaces;

/// <summary>
/// Interface do repositório para o CRUD de Animes.
/// </summary>
public interface IAnimeRepository
{
    /// <summary>
    /// Método para obter uma lista de todos os animes cadastrados.
    /// </summary>
    /// <returns>Uma lista de animes.</returns>
    Task<IEnumerable<Anime>> GetAnimesAsync();

    /// <summary>
    /// Método para obter uma lista de animes filtrados por <paramref name="diretor"/>, <paramref name="nome"/> e/ou <paramref name="palavrasChave"/>.
    /// </summary>
    /// <param name="nome"></param>
    /// <param name="diretor"></param>
    /// <param name="palavrasChave"></param>
    /// <returns>Uma lista de animes filtrados por <paramref name="diretor"/>, <paramref name="nome"/> e/ou <paramref name="palavrasChave"/>.</returns>
    Task<IEnumerable<Anime>> GetAnimesByFilterAsync(string? nome, string? diretor, List<string>? palavrasChave);

    /// <summary>
    /// Método para consultar o anime por <paramref name="id"/> de forma assíncrona.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>O anime cadastrado com <paramref name="id"/>.</returns>
    Task<Anime?> GetAnimeByIdAsync(int id);

    /// <summary>
    /// Método para consultar o anime por <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>O anime cadastrado com <paramref name="id"/>.</returns>
    Anime? GetAnimeById(int id);

    /// <summary>
    /// Cadastro de anime no banco de dados por meio dos atributos de <paramref name="anime"/>.
    /// </summary>
    /// <param name="anime"></param>
    Task AddAnimeAsync(Anime anime);

    /// <summary>
    /// Atualização de dados de um anime no banco de dados por meio dos atributos de <paramref name="anime"/>.
    /// </summary>
    /// <param name="anime"></param>
    void UpdateAnime(Anime anime);

    /// <summary>
    /// Deleção lógica de um anime por meio dos atributos de <paramref name="anime"/>.
    /// </summary>
    /// <param name="anime"></param>
    void DeleteAnime(Anime anime);
}
