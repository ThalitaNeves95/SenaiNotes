using APISenaiNotes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using APISenaiNotes.Models;

namespace APISenaiNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private ITagRepository _tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTags()
        {
            var tags = await _tagRepository.ListarTodos();
            return Ok(tags);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarTag(Tag tag)
        {
            await _tagRepository.Cadastrar(tag);
            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            var tag = await _tagRepository.BuscarPorId(id);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        [HttpGet("buscar/{titulo}")]
        public async Task<IActionResult> BuscarPorTitulo(string titulo)
        {
            var tags = await _tagRepository.BuscarTagPorTitulo(titulo);

            if (tags == null || !tags.Any())
            {
                return NotFound();
            }

            return Ok(tags);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, Tag tag)
        {
            try
            {
                await _tagRepository.Atualizar(id, tag);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _tagRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
