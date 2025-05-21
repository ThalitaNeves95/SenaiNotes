using APISenaiNotes.Models;
using SenaiNotes.Context;
using SenaiNotes.Models;
using System.Security.Cryptography;

namespace APISenaiNotes.Interfaces
{
    public interface IUsuarioRepository
    {
        void CadastrarUsuario(Usuario usuario);
        Usuario Login(string email, 
            string senha);
        Usuario BuscarPorId(int id);
        List<Usuario> ListarUsuarios();

    }
    public interface ISenhaRepository
    {
        string GerarHash(string senha);
        bool VerificarHash(string senha, string hash);
    }
    public class SenhaRepository : ISenhaRepository
    {
        public string GerarHash(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        public bool VerificarHash(string senha, string hash)
        {
            var senhaHash = GerarHash(senha);
            return senhaHash == hash;
        }
    }

    public class UsuarioRepository : IUsuarioRepository 
    {
        private SenaiNotesContext _context;
        private ISenhaRepository _senhaRepository;
        public UsuarioRepository(SenaiNotesContext context, ISenhaRepository senhaRepository)
        {
            _context = context;
            _senhaRepository = senhaRepository;
        }

        public Usuario BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            usuario.Senha = _senhaRepository.GerarHash(usuario.Senha);
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public List<Usuario> ListarUsuarios()
        {
            throw new NotImplementedException();
        }

        public Usuario Login(string email, string senha)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
            if (usuario != null && _senhaRepository.VerificarHash(senha, usuario.Senha))
            {
                return usuario;
            }

            return null;

        }
    }
}

        