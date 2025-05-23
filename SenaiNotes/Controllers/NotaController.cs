using APISenaiNotes.DTO;
using APISenaiNotes.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using APISenaiNotes.Models;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace APISenaiNotes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private INotaRepository _notaRepository;

        public NotaController(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository;
        }

        [SwaggerOperation(Summary = "Lista as notas.")]
        [HttpGet]
        public async Task<IActionResult> ListarNotas()
        {
            var notas = await _notaRepository.ListarTodos();
            return Ok(notas);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra uma nova nota.")]
        public async Task<IActionResult> CadastrarNota(CadastrarNotaDto notaDto)
        {
            await _notaRepository.CadastrarNotaDto(notaDto);

            return Created();
        }

        [SwaggerOperation(Summary = "Edita a nota.")]
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

        [SwaggerOperation(Summary = "Exclui a nota.")]
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

        [SwaggerOperation(Summary = "Arquiva ou desarquiva a nota.")]
        [HttpPatch("{id}/arquivar")]
        public async Task<IActionResult> ArquivarNota(int id)
        {
            await _notaRepository.ArquivarNota(id);
            return NoContent();
        }
    }
}
