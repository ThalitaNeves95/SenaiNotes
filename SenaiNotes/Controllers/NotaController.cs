using APISenaiNotes.DTO;
using APISenaiNotes.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using APISenaiNotes.Models;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using APISenaiNotes.Validator;

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

        [HttpGet("buscar/{nota}")]
        public async Task<IActionResult> BuscarNotaPorNomeAsync(string nota)
        {
            var nomeNota = await _notaRepository.BuscarNotaPorNomeAsync(nota);

            if (nomeNota == null)
            {
                return NotFound();
            }

            return Ok(nomeNota);
        }


        [HttpPost("sem-imagem")]
        [SwaggerOperation(Summary = "Cadastra uma nova nota sem imagem.")]
        public async Task<IActionResult> CadastrarNotaSemImagem(CadastrarNotaSemImagemDto notaDto)
        {
            await _notaRepository.CadastrarNotaSemImagemDto(notaDto);

            return Created();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra uma nova nota.")]
        

        public async Task<IActionResult> CadastrarNota(CadastrarNotaDto notaDto)
        {
            if (notaDto.ImagemAnotacao != null)
            {
                // EXTRA - Verificar se o arquivo é uma imagem
                // 1 - Criar uma variavel que vai ser a pasta de destino
                var pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), "ImagensNotas");

                // 2 - Salvar o arquivo
                // EXTRA - Criar um nome personalizado para o arquivo
                var nomeArquivo = notaDto.ImagemAnotacao.FileName;

                var caminhoArquivo = Path.Combine(pastaDestino, nomeArquivo);

                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    notaDto.ImagemAnotacao.CopyTo(stream);
                }

                // 3 - Guardar o local do arquivo

                notaDto.Imagem = caminhoArquivo;
            }

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
            catch (ArgumentNullException ex)
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
