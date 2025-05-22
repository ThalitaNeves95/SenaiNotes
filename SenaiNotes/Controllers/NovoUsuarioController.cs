using APISenaiNotes.Interfaces;
using APISenaiNotes.Models;
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
        public async Task<IActionResult> CadastrarNovoUsuario(Usuario usuario)
        {
            await _novoUsuarioRepository.Cadastrar(usuario);
            return Created();
        }
    }
}
