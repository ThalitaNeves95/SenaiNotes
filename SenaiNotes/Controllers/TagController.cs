using APISenaiNotes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using APISenaiNotes.Models;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace APISenaiNotes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private ITagRepository _tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [SwaggerOperation(Summary = "Lista as tags disponiveis.")]
        [HttpGet]
        public async Task<IActionResult> ListarTags()
        {
            var tags = await _tagRepository.ListarTodos();
            return Ok(tags);
        }

        [SwaggerOperation(Summary = "Cadastra uma nova tag.")]
        [HttpPost]
        public async Task<IActionResult> CadastrarTag(Tag tag)
        {
            await _tagRepository.Cadastrar(tag);
            return Created();
        }

        [SwaggerOperation(Summary = "Edita a tag.")]
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

        [SwaggerOperation(Summary = "Exclui a tag.")]
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
