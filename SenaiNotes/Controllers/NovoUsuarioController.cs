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

        // Todo metodo contrutor, tem que ter o mesmo nome da class
        public NovoUsuarioController(INovoUsuarioRepository novoUsuarioRepository)
        {
            _novoUsuarioRepository = novoUsuarioRepository;
        }

        [HttpGet]
        // IActionResult = Interface que vem do .net - Permite que um metodo retorne um status code
        public IActionResult ListarUsuarios()
        {
            return Ok(_novoUsuarioRepository.ListarTodos());
        }

        [HttpPost]
        public IActionResult CadastrarNovoUsuario(Usuario usuario)
        {
            // 1 - Coloco o cliente no Banco de Dados
            _novoUsuarioRepository.Cadastrar(usuario);
            // 3 - Retorno o resultado
            // 201 - Created
            return Created();
        }
    }
}
