using APISenaiNotes.Models;

namespace APISenaiNotes.DTO
{
    public class CadastrarNotaDto
    {
        public string Titulo { get; set; }
        public string? Imagem { get; set; }
        public string? Conteudo { get; set; }
        public int UsuarioId { get; set; }
        public IFormFile? ImagemAnotacao { get; set; }
        public virtual ICollection<string> Tags { get; set; } = new List<string>();

    }
}
