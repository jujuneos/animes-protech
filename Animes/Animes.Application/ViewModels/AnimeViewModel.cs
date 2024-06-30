using Animes.Domain.Entities;

namespace Animes.Application.ViewModels;

// ViewModel para mascarar os modelos de domínio
public class AnimeViewModel
{
    public IEnumerable<Anime>? Animes { get; set; }
}
