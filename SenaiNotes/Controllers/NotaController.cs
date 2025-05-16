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

        [HttpPost]
        public IActionResult CadastrarNota(Nota nota)
        {
            _notaRepository.Cadastrar(nota);

            return Created();
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            Nota nota = _notaRepository.BuscarPorId(id);

            if (nota == null)
            {
                // 404 - Não Encontrado
                return NotFound();
            }

            return Ok(nota);
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, Nota nota)
        {
            try
            {
                _notaRepository.Atualizar(id, nota);
                return Ok(nota);
            }
            catch (Exception ex)
            {

                return NotFound("Produto não encontrado!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _notaRepository.Deletar(id);
                return NoContent();
            }

            catch (Exception ex)
            {
                return NotFound("Nota não encontrada!");
            }
        }

        [HttpPatch("{id}/arquivar")]
        public async Task<IActionResult> AtualizarArquivado(int id, [FromBody] Nota nota)
        {
            await _notaRepository.ArquivarNota(id, nota.Arquivada);
            return NoContent();
        }
    }
}
