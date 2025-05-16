using System.ClientModel.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISenaiNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Listar todos os usuários");
        }
        [HttpGet("buscar/{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Listar o usuário com id {id}");
        }
        [HttpPost]
        public IActionResult Post([FromBody] string usuario)
        {
            return Created("", $"Usuário {usuario} cadastrado com sucesso");
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string usuario)
        {
            return Ok($"Usuário {usuario} atualizado com sucesso");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Usuário com id {id} deletado com sucesso");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] string usuario)
        {
            
            return Ok($"Usuário {usuario} logado com sucesso");

        }
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] string usuario)
        {
            
            return Created("", $"Usuário {usuario} cadastrado com sucesso");
        }
        [HttpGet("listar")]
        public IActionResult Listar()
        {
            
            return Ok("Listar todos os usuários");
    }
        
        [HttpPut("atualizar/{id}")]
        public IActionResult Atualizar(int id, [FromBody] string usuario)
        {
            
            return Ok($"Usuário {usuario} atualizado com sucesso");
        }
        [HttpDelete("deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            
            return Ok($"Usuário com id {id} deletado com sucesso");
}

    }
}


