using Animes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Animes.Infra.Data.Context;

// Arquivo de contexto do EF Core
public class AnimesDbContext : DbContext
{
    public AnimesDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Anime> Animes { get; set; }
    public DbSet<Login> Usuarios { get; set; }
}
