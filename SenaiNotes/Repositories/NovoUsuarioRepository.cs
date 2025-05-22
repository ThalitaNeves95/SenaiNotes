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

       public async Task Atualizar(int id, Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(id);

            if (usuarioExistente == null)
                throw new InvalidOperationException("Usuário não encontrado!");

            await _context.SaveChangesAsync();
        }

        public async Task CadastrarAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);

            await _context.SaveChangesAsync();
        }

        // public Usuario? BuscarPorEmailSenha(string email, string senha)
        // {
        //     // Encontrar o Cliente que possui o e-mail e senha fornecidos
        //     // Quando eu quero 1 só coisa, utilizo o FirstOrDefault
        //     var usuarioEncontrado = _context.Usuarios.FirstOrDefault(c => c.Email == email);

        //     // Caso não encontre, retorno nulo
        //     if(usuarioEncontrado == null)
        //     return null;

        //     var passwordService = new PasswordService();

        //     // Verificar se a senha do usuário gera a mesma Hash
        //     var resultado = passwordService.VerificarSenha(usuarioEncontrado, senha);

        //     if(resultado == true) return usuarioEncontrado;
        //     return null;
        // }

        public async Task Deletar(int id)
        {
            var usuarioEncontrado = await _context.Usuarios.FindAsync(id);

            if (usuarioEncontrado == null)
            {
                throw new ArgumentNullException("Usuário não encontrado!");
            }

            _context.Usuarios.Remove(usuarioEncontrado);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> ListarTodos()
        {
            return await _context.Usuarios.ToListAsync();
        }
    }
}
