using APISenaiNotes.Models;

namespace APISenaiNotes.DTO
{
    public class CadastrarNotaDto
    {
        public int NotaId { get; set; }

        public int UsuarioId { get; set; }

        public string? Titulo { get; set; }

        public string? Imagem { get; set; }

        public string? Conteudo { get; set; }

        public virtual ICollection<string> Tags { get; set; } = new List<string>();
    }
}
