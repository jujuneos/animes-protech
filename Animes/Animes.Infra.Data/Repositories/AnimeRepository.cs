using Animes.Domain.Entities;
using Animes.Domain.Interfaces;
using Animes.Infra.Data.Context;

namespace Animes.Infra.Data.Repositories;

// Repositório que implementa a interface IAnimeRepository, e instancia a classe de contexto
public class AnimeRepository : IAnimeRepository
{
    public AnimesDbContext _context { get; set; }

    public AnimeRepository(AnimesDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Anime> GetAnimes()
    {
        return _context.Animes;
    }
}
