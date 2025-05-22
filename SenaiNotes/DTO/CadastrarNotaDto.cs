using APISenaiNotes.Models;

namespace APISenaiNotes.DTO
{
    public class CadastrarNotaDto
    {
        public int NotaId { get; set; }

        public int? UsuarioId { get; set; }

        public string? Titulo { get; set; }

        public string? Imagem { get; set; }

        public string? Conteudo { get; set; }

        public bool? Excluida { get; set; }

        public bool? Arquivada { get; set; }

        public DateTime? DataCriacao { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
