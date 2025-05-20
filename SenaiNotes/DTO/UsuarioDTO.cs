namespace APISenaiNotes.DTO
{
    public class UsuarioDTO

    {
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public DateTime? DataCriacao { get; set; }

    }
}
