using Animes.Domain.Entities;
using Animes.Domain.Interfaces;
using Animes.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Animes.Infra.Data.Repositories;

// Repositório que implementa a interface IAnimeRepository, e instancia a classe de contexto
public class AnimeRepository : IAnimeRepository
{
    public AnimesDbContext _context { get; set; }

    public AnimeRepository(AnimesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Anime>> GetAnimesAsync()
    {
        return await _context.Animes.Where(a => a.Deletado == false).ToListAsync();
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

        return await query.ToListAsync();
    }

    public async Task<Anime?> GetAnimeByIdAsync(int id)
    {
        return await _context.Animes.FindAsync(id);
    }

    public Anime? GetAnimeById(int id)
    {
        var anime = _context.Animes.Find(id);
        if (anime == null) { return null; }
        return anime;
    }

    public async Task<Anime?> GetAnimeByNameAsync(string name)
    {
        return await _context.Animes.FindAsync(name);
    }

    public async Task AddAnimeAsync(Anime anime)
    {
        await _context.Animes.AddAsync(anime);
        await _context.SaveChangesAsync();
    }

    public void UpdateAnime(Anime anime)
    {
        _context.Animes.Update(anime);
        _context.SaveChanges();
    }

    public void DeleteAnime(Anime anime)
    {
        _context.Animes.Update(anime);
        _context.SaveChanges();
    }
}
