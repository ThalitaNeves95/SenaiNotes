using APISenaiNotes.Interfaces;
using APISenaiNotes.Models;
using APISenaiNotes.Context;

namespace APISenaiNotes.Repositories
{
    public class NovoUsuarioRepository : INovoUsuarioRepository
    {
        private readonly SenaiNotesContext _context;

        public NovoUsuarioRepository(SenaiNotesContext context)
        {
            _context = context;
        }

        public void Atualizar(int id, Usuario cliente)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> BuscarClientePorNome(string nome)
        {
            //Where - traz todos que atendem uma condicao
            //var listaClientes = _context.Clientes.Where(c => c.NomeCompleto == nome).ToList();
            var listarUsuarios = _context.Usuarios.Where(c => c.NomeUsuario.Contains(nome)).ToList();

            return listarUsuarios;
        }

        public Usuario? BuscarPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(c => c.UsuarioId == id);
        }

        public void Cadastrar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            // 2 - Salvo a Alteração
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var usuarioEncontrado = _context.Usuarios.Find(id);

            if (usuarioEncontrado == null)
            {
                throw new ArgumentNullException("Usuário não encontrado!");
            }

            _context.Usuarios.Remove(usuarioEncontrado);

            _context.SaveChanges();
        }

        public List<Usuario> ListarTodos()
        {
            // Select permite que eu selecione quais campos eu quero pegar
            return _context.Usuarios.ToList();
        }
    }
}
