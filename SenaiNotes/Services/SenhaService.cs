namespace APISenaiNotes.Services
{
    public class SenhaService
    {
        public SenhaService() { }
        public SenhaService(string name) {
            Name = name;
        }
        public string Name { get; set; } = null!;
        public string HashSenha(string senha)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        public bool VerificarHash(string senha, string hash)
        {
            var senhaHash = HashSenha(senha);
            return senhaHash == hash;
        }
        public string GerarHash(string senha)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

    }
}
