using Animes.Application.Interfaces;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces;

namespace Animes.Application.Services;

// Service de animes, que implementa a interface IAnimeService e dá corpo ao método GetAnimes
public class AnimeService : IAnimeService
{
    public IAnimeRepository _animeRepository;

    public AnimeService(IAnimeRepository animeRepository)
    {
        _animeRepository = animeRepository;
    }

    public async Task<IEnumerable<Anime>> GetAnimesAsync()
    {
        return await _animeRepository.GetAnimesAsync();
    }

    public async Task<IEnumerable<Anime>> GetAnimesByFilterAsync(string? nome, string? diretor, List<string>? palavrasChave)
    {
        return await _animeRepository.GetAnimesByFilterAsync(nome, diretor, palavrasChave);
    }

    public async Task<Anime?> GetAnimeByIdAsync(int id)
    {
        return await _animeRepository.GetAnimeByIdAsync(id);
    }

    public async Task AddAnimeAsync(Anime anime)
    {
        await _animeRepository.AddAnimeAsync(anime);
    }

    public void UpdateAnime(int id, Anime anime)
    {
        var animeExistente = _animeRepository.GetAnimeById(id);
        if (animeExistente == null)
        {
            throw new Exception();
        }

        animeExistente.Nome = anime.Nome;
        animeExistente.Resumo = anime.Resumo;
        animeExistente.Diretor = anime.Diretor;

        _animeRepository.UpdateAnime(animeExistente);
    }

    public void DeleteAnime(int id)
    {
        var animeExistente = _animeRepository.GetAnimeById(id);
        if (animeExistente == null)
        {
            throw new Exception();
        }

        animeExistente.Deletado = true;

        _animeRepository.DeleteAnime(animeExistente);
    }
}
