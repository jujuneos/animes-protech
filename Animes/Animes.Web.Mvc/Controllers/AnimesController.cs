using Animes.Application.Interfaces;
using Animes.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Animes.Web.Mvc.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimesController : Controller
{
    public IAnimeService _animeService { get; set; }

    public AnimesController(IAnimeService animeService)
    {
        _animeService = animeService;
    }

    /// <summary>
    /// Lista todos os registros não deletados com paginação por meio de <paramref name="pagina"/> e <paramref name="quantidadeRegistros"/>.
    /// </summary>
    /// <param name="pagina">Recebe o número da página que o usuário deseja consultar.</param>
    /// <param name="quantidadeRegistros">Recebe a quantidade de registros que o usuário quer ver da determinada página.</param>
    /// <returns>Uma lista de registros paginados.</returns>
    [HttpGet, Authorize]
    public async Task<ActionResult<IEnumerable<Anime>>> ListarTodos(int pagina, int quantidadeRegistros)
    {
        var animes = await _animeService.GetAnimesAsync();

        if (pagina > 0 && quantidadeRegistros > 0)
        {
            animes = animes
                .Skip((pagina - 1) * quantidadeRegistros)
                .Take(quantidadeRegistros);
        }
        return Ok(animes);
    }

    /// <summary>
    /// Lista os animes cadastrados paginados por meio de <paramref name="pagina"/> e <paramref name="quantidadeRegistros"/> e filtrados por <paramref name="nome"/>, <paramref name="diretor"/> e <paramref name="palavrasChave"/>.
    /// </summary>
    /// <param name="pagina">Recebe o número da página que o usuário deseja consultar.</param>
    /// <param name="quantidadeRegistros">Recebe a quantidade de registros que o usuário quer ver da determinada página.</param>
    /// <param name="nome">O nome do anime que o usuário deseja consultar.</param>
    /// <param name="diretor">O diretor do anime que o usuário deseja consultar.</param>
    /// <param name="palavrasChave">Palavras chave que deverão constar no resumo do anime desejado.</param>
    /// <returns>Uma lista de registros filtrados e paginados.</returns>
    /// <exception cref="Exception">Lançada se algum dos parâmetros for inválido.</exception>
    [HttpGet, Route("filtro"), Authorize]
    public async Task<ActionResult<IEnumerable<Anime>>> ListarPorFiltro(int pagina, int quantidadeRegistros, [FromQuery] string? nome, [FromQuery] string? diretor, [FromQuery] List<string>? palavrasChave)
    {
        try
        {
            var animes = await _animeService.GetAnimesByFilterAsync(nome, diretor, palavrasChave);

            if (pagina > 0 && quantidadeRegistros > 0)
            {
                animes = animes
                    .Skip((pagina - 1) * quantidadeRegistros)
                    .Take(quantidadeRegistros);
            }

            return Ok(animes);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno.");
        }
    }

    // Método para cadastrar um novo anime
    [HttpPost, Authorize]
    public async Task<ActionResult> CadastrarAnime(Anime anime)
    {
        await _animeService.AddAnimeAsync(anime);
        return Ok();
    }

    // Método para atualizar os dados de um anime.
    [HttpPut, Route("id:{id}"), Authorize]
    public ActionResult AtualizarAnime(int id, Anime anime)
    {
        _animeService.UpdateAnime(id, anime);
        return Ok();
    }

    // Método para deletar logicamente um anime. A exclusão ocorre marcando a Flag deletado como true.
    [HttpDelete, Route("id:{id}"), Authorize]
    public ActionResult DeletarAnime(int id)
    {
        _animeService.DeleteAnime(id);
        return Ok();
    }
}
