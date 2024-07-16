using Animes.Application.Interfaces;
using Animes.Application.Services;
using Animes.Domain.Interfaces;
using Animes.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Animes.Infrastructure.IoC;

/// <summary>
/// Classe <c>DependencyContainer</c> usa injeção de dependência para fazer as conexões entre as interfaces e as implementações.
/// </summary>
public class DependencyContainer
{
    /// <summary>
    /// Método <c>RegisterServices</c> registra os services para uso na Classe Program.
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IAnimeService, AnimeService>();
        services.AddScoped<IAnimeRepository, AnimeRepository>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<ILoginRepository, LoginRepository>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
    }
}
