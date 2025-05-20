using APISenaiNotes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SenaiNotes.Models;
using System;

namespace APISenaiNotes.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class LembreteController : ControllerBase
        {
        

            private readonly ILembreterRepository _repo;

            public LembreteController(ILembreterRepository repo)
            {
                _repo = repo;
            }
            [HttpPost]
            public IActionResult CadastrarLembrete(Lembrete lembrete)
            {
                try
                {
                    _repo.Cadastrar(lembrete);
                    return CreatedAtAction(nameof(ListarTodos), new { id = lembrete.LembreteId }, lembrete);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            [HttpGet]
            public IActionResult ListarTodos()
            {
                try
                {
                    return Ok(_repo.ListarTodos());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            [HttpGet("{id}")]
            public IActionResult ListarPorId(int id)
            {
                try
                {
                    Lembrete lembrete = _repo.BuscarPorId(id);
                    if (lembrete == null)
                    {
                        return NotFound();
                    }
                    return Ok(lembrete);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            [HttpPut("{id}")]
            public IActionResult Atualizar(int id, Lembrete lembrete)
            {
                try
                {
                    _repo.Atualizar(id, lembrete);
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
                    _repo.Deletar(id);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}


