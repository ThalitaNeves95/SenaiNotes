using APISenaiNotes.DTO;
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
        public async Task<IActionResult> ListarNotas()
        {
            var notas = await _notaRepository.ListarTodos();
            return Ok(notas);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarNota(CadastrarNotaDto nota)
        {
            await _notaRepository.Cadastrar(nota);
            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            var nota = await _notaRepository.BuscarPorId(id);

            if (nota == null)
            {
                return NotFound();
            }

            return Ok(nota);
        }

        [HttpGet("buscar/{titulo}")]
        public async Task<IActionResult> BuscarNotaPorTexto(string titulo)
        {
            var notas = await _notaRepository.BuscarNotaPorTexto(titulo);

            if (notas == null || !notas.Any())
            {
                return NotFound();
            }

            return Ok(notas);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, CadastrarNotaDto nota)
        {
            try
            {
                await _notaRepository.Atualizar(id, nota);
                return Ok(nota);
            }
            catch (Exception ex)
            {
                return NotFound("Nota não encontrada!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _notaRepository.Deletar(id);
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
