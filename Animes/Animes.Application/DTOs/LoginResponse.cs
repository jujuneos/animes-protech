namespace Animes.Application.DTOs;

public class LoginResponse
{
    public string? Token { get; set; }
    public DateTime Expiracao { get; set; }
}