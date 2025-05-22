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
