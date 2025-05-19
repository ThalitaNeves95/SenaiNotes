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


        Usuario IUsuarioRepository.Login(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);

        }

        Usuario IUsuarioRepository.BuscarPorId(int id)
        {
            return _context.Usuarios.Find(id);
        }

        List<Usuario> IUsuarioRepository.ListarUsuarios()
        {
            return _context.Usuarios.ToList();
        }
    }
}
