using Animes.Application.ViewModels;

namespace Animes.Application.Interfaces;

// Interface do método para receber uma listagem dos animes
public interface IAnimeService
{
    AnimeViewModel GetAnimes();
}
