using Animes.Application.Interfaces;
using Animes.Application.ViewModels;
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

    public AnimeViewModel GetAnimes()
    {
        return new AnimeViewModel()
        {
            Animes = _animeRepository.GetAnimes()
        };
    }
}
