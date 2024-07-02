using Animes.Application.DTOs;
using Animes.Application.Interfaces;
using Animes.Application.Services;
using Animes.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Animes.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private ILoginService _loginService { get; set; }

    public AuthController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = _loginService.Authenticate(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Login não autorizado.");
        }
    }

    [HttpPost("cadastrar")]
    public ActionResult Cadastrar(Login user)
    {
        _loginService.AddUser(user);
        return Ok();
    }
}