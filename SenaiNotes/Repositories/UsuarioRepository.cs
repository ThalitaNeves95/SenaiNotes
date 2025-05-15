using APISenaiNotes.Interfaces;
using APISenaiNotes.Models;
using SenaiNotes.Context;
namespace APISenaiNotes.Repositories
{
    public class UsuarioRepository : IUsuarioRepository

{
    private SenaiNotesContext _context;
    public UsuarioRepository(SenaiNotesContext context)
    {
        _context = context;
    }
    public void CadastrarUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }
    public Usuario Login(string email, string senha)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
    }
    public Usuario BuscarPorId(int id)
    {
        return _context.Usuarios.Find(id);
    }
    public List<Usuario> ListarUsuarios()
    {
        return _context.Usuarios.ToList();
    }

        public void CadastrarOUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        Usuario IUsuarioRepository.Login(string email, string senha)
        {
            throw new NotImplementedException();
        }

        Usuario IUsuarioRepository.BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        List<Usuario> IUsuarioRepository.ListarUsuarios()
        {
            throw new NotImplementedException();
        }
    }
}
