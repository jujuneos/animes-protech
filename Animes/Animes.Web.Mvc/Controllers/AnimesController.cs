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

    // Listagem de todos os registros não deletados com paginação
    // A variável pagina recebe o número da página que o usuário deseja consultar
    // A variável quandtidadeRegistros recebe a quantidade que o usuário quer ver da determinada página
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

    // Listagem com filtro e paginação
    // A lista palavrasChave recebe várias palavras de uma vez e retorna todos os registros cujo Resumo contenha alguma delas.
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
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
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
