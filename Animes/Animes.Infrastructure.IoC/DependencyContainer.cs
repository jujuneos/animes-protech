using Animes.Application.Interfaces;
using Animes.Application.Services;
using Animes.Domain.Interfaces;
using Animes.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Animes.Infrastructure.IoC;

// Uso de injeção de dependência para fazer as conexões entre as interfaces e as implementações
public class DependencyContainer
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IAnimeService, AnimeService>();
        services.AddScoped<IAnimeRepository, AnimeRepository>();
    }
}
