
using Microsoft.AspNetCore.Mvc;
using APISenaiNotes.Services;

namespace APISenaiNotes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        
        if (model.Username == "admin" && model.Password == "senha123")
        {
            var token = _authService.GenerateToken(model.Username);
            return Ok(new { Token = token });
        }

        return Unauthorized("Credenciais inválidas");
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}