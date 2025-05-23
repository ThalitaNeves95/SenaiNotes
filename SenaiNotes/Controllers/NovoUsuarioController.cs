using APISenaiNotes.DTO;
using APISenaiNotes.Interfaces;
using APISenaiNotes.Models;
using APISenaiNotes.Services;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
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

        [SwaggerOperation(Summary = "Lista os usuários da base.")]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _novoUsuarioRepository.ListarTodos();
            return Ok(usuarios);
        }

        [SwaggerOperation(Summary = "Cria um novo usuário.")]
        [HttpPost]
        public async Task<IActionResult> CadastrarNovoUsuario(NovoUsuarioDto usuario)
        {
            await _novoUsuarioRepository.Cadastrar(usuario);
            return Created();
        }

        [SwaggerOperation(Summary = "Efetua login na aplicação.")]
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

            var json = new { token, usuario };

            return Ok(json);
        }

        [SwaggerOperation(Summary = "Edita as informações do usuário.")]
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, NovoUsuarioDto usuario)
        {
            try
            {
                _novoUsuarioRepository.Atualizar(id, usuario);
                return Ok(usuario);
            }
            catch (ArgumentNullException ex)
            {

                return NotFound("Usuário não encontrado!");
            }
        }

        [SwaggerOperation(Summary = "Exclui o usuário da base.")]
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _novoUsuarioRepository.Deletar(id);
                return NoContent();
            }

            catch (ArgumentNullException ex)
            {
                return NotFound("Usuário não encontrado!");
            }
        }
    }
}
