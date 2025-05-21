namespace APISenaiNotes.DTO
{
    public class CadastrarNotaDto
    {
        public string Titulo { get; set; }
        public string? Conteudo { get; set; }
        public string? Imagem { get; set; }
        public int UsuarioId { get; set; }
        public int CategoriaId { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
