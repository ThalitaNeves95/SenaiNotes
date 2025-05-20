using APISenaiNotes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SenaiNotes.Models;

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
        public IActionResult ListarTags()
        {
            return Ok(_tagRepository.ListarTodos());
        }

        [HttpPost]
        public IActionResult CadastrarTag(Tag tag)
        {
            _tagRepository.Cadastrar(tag);
            return Created();
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            Tag tag = _tagRepository.BuscarPorId(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        [HttpGet("buscar/{titulo}")]
        public IActionResult BuscarPorTitulo(string titulo)
        {
            var tag = _tagRepository.BuscarTagPorTitulo(titulo);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Tag tag)
        {
            try
            {
                _tagRepository.Atualizar(id, tag);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _tagRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
