using APISenaiNotes.DTO;
using APISenaiNotes.Interfaces;
using APISenaiNotes.Models;
using APISenaiNotes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APISenaiNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovoUsuarioController : ControllerBase
    {
        private INovoUsuarioRepository _novoUsuarioRepository;

        public NovoUsuarioController(INovoUsuarioRepository novoUsuarioRepository)
        {
            _novoUsuarioRepository = novoUsuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _novoUsuarioRepository.ListarTodos();
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarNovoUsuario(NovoUsuarioDto usuario)
        {
            await _novoUsuarioRepository.Cadastrar(usuario);
            return Created();
        }

        [HttpPost("login")]
        public async Task <IActionResult> Login(LoginDto login)
        {
            var usuario = await _novoUsuarioRepository.Login(login.Email, login.Senha);

            if (usuario == null)
            {
                return Unauthorized("E-mail ou senha inválidos!");
            }

            var tokenService = new TokenService();

            var token = tokenService.GenerateToken(usuario.Email);

            var json = new { Token = token };

            return Ok(json);
        }
    }
}
