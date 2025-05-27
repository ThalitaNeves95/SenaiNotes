namespace APISenaiNotes.DTO
{
    public class CadastrarNotaSemImagemDto
    {
        public string Titulo { get; set; }
        public string? Imagem { get; set; }
        public string? Conteudo { get; set; }
        public int UsuarioId { get; set; }
        public virtual ICollection<string> Tags { get; set; } = new List<string>();
    }
}
