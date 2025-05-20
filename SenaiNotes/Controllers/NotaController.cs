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
            _notaRepository = notaRepository;
        }

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
                return NotFound();
            }

            return Ok(nota);
        }

        [HttpGet("buscar/{titulo}")]
        public IActionResult BuscarPorTitulo(string titulo)
        {
            var nota = _notaRepository.BuscarNotaPorTitulo(titulo);

            if (nota == null)
            {
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

                return NotFound("Nota não encontrado!");
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
        public async Task<IActionResult> ArquivarNota(int id)
        {
            await _notaRepository.ArquivarNota(id);
            return NoContent();
        }
    }
}
