namespace APISenaiNotes.Interfaces.DTO
{
    public class CadastrarUsuarioDTO
    {
        public string NomeUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public DateTime? DataCriacao { get; set; }
    }
}
