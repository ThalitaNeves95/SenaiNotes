using APISenaiNotes.Models;
using SenaiNotes.Models;
using System.Security.Cryptography;

namespace APISenaiNotes.Interfaces
{
    public interface IUsuarioRepository
    {
        void CadastrarUsuario(Usuario usuario);
        Usuario Login(string email, string senha);
        Usuario BuscarPorId(int id);
        List<Usuario> ListarUsuarios();

    }
}