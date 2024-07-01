using Animes.Domain.Entities;

namespace Animes.Domain.Interfaces;

// Interface para o repositório de animes
public interface IAnimeRepository
{
    Task<IEnumerable<Anime>> GetAnimesAsync();
    Task<IEnumerable<Anime>> GetAnimesByFilterAsync(string? nome, string? diretor, List<string>? palavrasChave);
    Task<Anime?> GetAnimeByIdAsync(int id);
    Anime? GetAnimeById(int id);
    Task AddAnimeAsync(Anime anime);
    void UpdateAnime(Anime anime);
    void DeleteAnime(Anime anime);
}
