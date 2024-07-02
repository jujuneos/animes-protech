using Animes.Application.Services;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces;
using Moq;
using System.Xml.Linq;
using Xunit;

namespace Animes.Tests;

public class AnimeTests
{
    private readonly Mock<IAnimeRepository> _animeRepositoryMock;
    private readonly AnimeService _animeService;

    public AnimeTests()
    {
        _animeRepositoryMock = new Mock<IAnimeRepository>();
        _animeService = new AnimeService(_animeRepositoryMock.Object);
    }

    // Testa uma consulta por um Id válido
    [Fact]
    public async Task ConsultaPorIdValida()
    {
        // Arrange
        var id = 1;
        var anime = new Anime
        {
            Id = id,
            Nome = "Naruto",
            Resumo = "Um jovem tenta se tornar Hokage",
            Diretor = "Masashi Kishimoto",
            Deletado = false
        };

        _animeRepositoryMock.Setup(repo => repo.GetAnimeByIdAsync(id)).ReturnsAsync(anime);

        // Act
        var result = await _animeService.GetAnimeByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal("Naruto", result.Nome);
        Assert.Equal("Um jovem tenta se tornar Hokage", result.Resumo);
        Assert.Equal("Masashi Kishimoto", result.Diretor);
        Assert.Equal(false, result.Deletado);
    }

    // Testa uma consulta por um Id inválido
    [Fact]
    public async Task ConsultaPorIdInvalida()
    {
        // Arrange
        var id = 900;

        _animeRepositoryMock.Setup(repo => repo.GetAnimeByIdAsync(id)).ReturnsAsync((Anime)null);

        // Act
        var result = await _animeService.GetAnimeByIdAsync(id);

        // Assert
        Assert.Null(result);
    }

    // Testa uma consulta por todos os animes cadastrados
    [Fact]
    public async Task ConsultaPorTodosRetornaTodos()
    {
        // Arrange
        var animes = new List<Anime>
        {
            new Anime { Id = 1, Nome = "Naruto", Resumo = "Um jovem tenta se tornar Hokage", Diretor = "Masashi Kishimoto", Deletado = false },
            new Anime { Id = 2, Nome = "One Piece", Resumo = "Um jovem tentar se tornar o rei dos piratas", Diretor = "Eichiro Oda", Deletado = false }
        };

        _animeRepositoryMock.Setup(repo => repo.GetAnimesAsync()).ReturnsAsync(animes);

        // Act
        var result = await _animeService.GetAnimesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Naruto", result.First().Nome);
    }

    // Testa uma consulta usando filtros
    [Fact]
    public async Task ConsultaComFiltro()
    {
        // Arrange
        var animes = new List<Anime>
        {
            new Anime { Id = 2, Nome = "One Piece", Resumo = "Um jovem tentar se tornar o rei dos piratas", Diretor = "Eichiro Oda", Deletado = false }
        };

        string nome = "One Piece";
        string diretor = "Eichiro Oda";
        List<string> palavrasChave = [ "rei", "pirata" ];

        _animeRepositoryMock.Setup(repo => repo.GetAnimesByFilterAsync(nome, diretor, palavrasChave)).ReturnsAsync(animes);

        // Act
        var result = await _animeService.GetAnimesByFilterAsync(nome, diretor, palavrasChave);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("One Piece", result.First().Nome);
    }

    // Testa o cadastro de um anime válido
    [Fact]
    public async Task AdicionarAnimeValido()
    {
        // Arrange
        var anime = new Anime { Nome = "Naruto", Resumo = "Um jovem tenta se tornar Hokage", Diretor = "Masashi Kishimoto", Deletado = false };

        // Act
        await _animeService.AddAnimeAsync(anime);

        // Assert
        _animeRepositoryMock.Verify(repo => repo.AddAnimeAsync(It.IsAny<Anime>()), Times.Once);
    }

    // Testa a edição de um anime válido
    [Fact]
    public void AtualizarAnimeValido()
    {
        // Arrange
        var anime = new Anime { Id = 1, Nome = "Narute", Resumo = "Um jovem tenta se tornar Raposa de Nove Caudas", Diretor = "Masashi Oda", Deletado = false };

        // Este bloco trata uma exceção que é lançada quando o anime com dado id não é encontrado
        try
        {
            // Act
            _animeService.UpdateAnime(1, anime);

            // Assert
            _animeRepositoryMock.Verify(repo => repo.UpdateAnime(It.IsAny<Anime>()), Times.Once);
        }
        catch (Exception ex) 
        {
            // Assert
            Assert.IsType<Exception>(ex);
        }
    }

    // Testa a exclusão de um anime
    [Fact]
    public void DeletarAnimeValido()
    {
        // Arrange
        var anime = new Anime { Id = 1, Nome = "Naruto", Resumo = "Um jovem tenta se tornar Hokage", Diretor = "Masashi Kishimoto", Deletado = false };

        try
        {
            // Act
            _animeService.DeleteAnime(anime.Id);

            // Assert
            _animeRepositoryMock.Verify(repo => repo.DeleteAnime(anime), Times.Once);
        }
        catch (Exception ex)
        {
            // Assert
            Assert.IsType<Exception>(ex);
        }
    }

    // Testa a exclusão de um anime inválido
    [Fact]
    public void DeletarAnimeInvalido()
    {
        // Arrange
        var anime = new Anime { Id = 800, Nome = "Nao Existe", Resumo = "Teste", Diretor = "Nenhum", Deletado = false };

        try
        {
            // Act
            _animeService.DeleteAnime(anime.Id);

            // Assert
            Assert.Null(anime);
        }
        catch (Exception ex)
        {
            // Assert
            Assert.IsType<Exception>(ex);
        }
    }
}
