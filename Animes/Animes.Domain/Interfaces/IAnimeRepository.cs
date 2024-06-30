using Animes.Domain.Entities;

namespace Animes.Domain.Interfaces;

// Interface para o repositório de animes
public interface IAnimeRepository
{
    IEnumerable<Anime> GetAnimes();
}
