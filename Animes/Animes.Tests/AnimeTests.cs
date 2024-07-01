using Animes.Application.Services;
using Animes.Domain.Entities;
using Animes.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        Xunit.Assert.NotNull(result);
        Xunit.Assert.Equal(id, result.Id);
        Xunit.Assert.Equal("Naruto", result.Nome);
        Xunit.Assert.Equal("Um jovem tenta se tornar Hokage", result.Resumo);
        Xunit.Assert.Equal("Masashi Kishimoto", result.Diretor);
        Xunit.Assert.Equal(false, result.Deletado);
    }

    [Fact]
    public async Task ConsultaPorIdInvalida()
    {
        // Arrange
        var id = 900;

        _animeRepositoryMock.Setup(repo => repo.GetAnimeByIdAsync(id)).ReturnsAsync((Anime)null);

        // Act
        var result = await _animeService.GetAnimeByIdAsync(id);

        // Assert
        Xunit.Assert.Null(result);
    }

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
        Xunit.Assert.NotNull(result);
        Xunit.Assert.Equal(2, result.Count());
        Xunit.Assert.Equal("Naruto", result.First().Nome);
    }

    [Fact]
    public async Task ConsultaComFiltro()
    {
        // Arrange
        var animes = new List<Anime>
        {
            new Anime { Id = 1, Nome = "Naruto", Resumo = "Um jovem tenta se tornar Hokage", Diretor = "Masashi Kishimoto", Deletado = false },
            new Anime { Id = 2, Nome = "One Piece", Resumo = "Um jovem tentar se tornar o rei dos piratas", Diretor = "Eichiro Oda", Deletado = false }
        };

        string nome = "One Piece";
        string diretor = "Eichiro Oda";
        List<string> palavrasChave = [ "pirata", "jovem" ];

        _animeRepositoryMock.Setup(repo => repo.GetAnimesByFilterAsync(nome, diretor, palavrasChave)).ReturnsAsync(animes);

        // Act
        var result = await _animeService.GetAnimesByFilterAsync(nome, diretor, palavrasChave);

        // Assert
        Xunit.Assert.NotNull(result);
        Xunit.Assert.Single(result);
        Xunit.Assert.Equal("One Piece", result.First().Nome);
    }

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

    [Fact]
    [ExpectedException(typeof(Exception))]
    public void AtualizarAnimeValido()
    {
        // Arrange
        var anime = new Anime { Nome = "Naruto", Resumo = "Um jovem tenta se tornar Raposa de Nove Caudas", Diretor = "Masashi Kishimoto", Deletado = false };

        // Act
        _animeService.UpdateAnime(1, anime);

        // Assert
        _animeRepositoryMock.Verify(repo => repo.UpdateAnime(It.IsAny<Anime>()), Times.Once);
    }

    [Fact]
    public void DeletarAnimeValido()
    {
        // Arrange
        var anime = new Anime { Id = 1, Nome = "Naruto", Resumo = "Um jovem tenta se tornar Hokage", Diretor = "Masashi Kishimoto", Deletado = false };

        // Act
        _animeService.DeleteAnime(anime.Id);

        // Assert
        _animeRepositoryMock.Verify(repo => repo.DeleteAnime(anime), Times.Once);
    }

    [Fact]
    public void DeletarAnimeInvalido()
    {
        // Arrange
        var anime = new Anime { Id = 800, Nome = "Nao Existe", Resumo = "Teste", Diretor = "Nenhum", Deletado = false };

        // Act
        _animeService.DeleteAnime(anime.Id);

        // Assert
        Xunit.Assert.Null(anime);
    }
}
