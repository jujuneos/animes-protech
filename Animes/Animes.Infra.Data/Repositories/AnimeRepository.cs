using Animes.Domain.Entities;
using Animes.Domain.Interfaces;
using Animes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Animes.Infra.Data.Repositories;

// Repositório que implementa a interface IAnimeRepository, e instancia a classe de contexto e o registro de Logs
public class AnimeRepository : IAnimeRepository
{
    private readonly AnimesDbContext _context;
    private readonly ILogger<AnimeRepository> _logger;

    public AnimeRepository(AnimesDbContext context, ILogger<AnimeRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Anime>> GetAnimesAsync()
    {
        var query = await _context.Animes.Where(a => a.Deletado == false).ToListAsync();

        _logger.LogInformation("Consulta realizada por todos os animes cadastrados.");

        return query;
    }

    public async Task<IEnumerable<Anime>> GetAnimesByFilterAsync(string? nome, string? diretor, List<string>? palavrasChave)
    {
        var query = _context.Animes.Where(a => a.Deletado == false).AsQueryable();

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(a => a.Nome!.Contains(nome));

        if (!string.IsNullOrEmpty(diretor))
            query = query.Where(a => a.Diretor!.Contains(diretor));

        if (palavrasChave!.Count > 0)
            foreach (string palavra in palavrasChave)
                query = query.Where(a => a.Resumo!.Contains(palavra));

        _logger.LogInformation("Consulta realizada por todos os animes utilizando filtros.");

        return await query.ToListAsync();
    }

    public async Task<Anime?> GetAnimeByIdAsync(int id)
    {
        return await _context.Animes.FindAsync(id);
    }

    public Anime? GetAnimeById(int id)
    {
        var anime = _context.Animes.Find(id);
        if (anime == null)
        {
            _logger.LogInformation("Anime com id {id} não encontrado.", id);
            return null; 
        }

        _logger.LogInformation("Anime encontrado: {anime}", anime.Nome);

        return anime;
    }

    public async Task AddAnimeAsync(Anime anime)
    {
        await _context.Animes.AddAsync(anime);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Novo anime adicionado: {anime}", anime.Nome);
    }

    public void UpdateAnime(Anime anime)
    {
        _context.Animes.Update(anime);
        _context.SaveChanges();

        _logger.LogInformation("Anime atualizado: {anime}", anime.Nome);
    }

    public void DeleteAnime(Anime anime)
    {
        _context.Animes.Update(anime);
        _context.SaveChanges();

        _logger.LogInformation("Anime deletado: {anime}", anime.Nome);
    }
}
