using APISenaiNotes.Models;

namespace APISenaiNotes.DTO
{
    public class CadastrarNotaDto
    {
        public string Titulo { get; set; }

        public string? Imagem { get; set; }

        public string? Conteudo { get; set; }

        public IFormFile? ImagemArquivo { get; set; }
        public virtual ICollection<string> Tags { get; set; } = new List<string>();


    }
}
