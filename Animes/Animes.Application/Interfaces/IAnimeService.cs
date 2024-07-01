using Animes.Domain.Entities;

namespace Animes.Application.Interfaces;

// Interface com os métodos CRUD dos Animes
public interface IAnimeService
{
    Task<IEnumerable<Anime>> GetAnimesAsync();
    Task<IEnumerable<Anime>> GetAnimesByFilterAsync(string? nome, string? diretor, List<string>? palavrasChave);
    Task<Anime?> GetAnimeByIdAsync(int id);
    Task AddAnimeAsync(Anime anime);
    void UpdateAnime(int id, Anime anime);
    void DeleteAnime(int id);
}
