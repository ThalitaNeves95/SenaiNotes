using APISenaiNotes.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SenaiNotes.Models;

namespace APISenaiNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private INotaRepository _notaRepository;

        public NotaController(INotaRepository notaRepository)
        {
            // Criar o repository não deve ser uma responsabilidade do Controller
            _notaRepository = notaRepository;
        }

        // GET - Listar
        [HttpGet]
        public IActionResult ListarNotas()
        {
            return Ok(_notaRepository.ListarTodos());
        }
    }
}
