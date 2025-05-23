using APISenaiNotes.Interfaces;
using APISenaiNotes.Models;
using APISenaiNotes.Context;
using Microsoft.EntityFrameworkCore;
using APISenaiNotes.DTO;
using APISenaiNotes.Services;

namespace APISenaiNotes.Repositories
{
    public class NovoUsuarioRepository : INovoUsuarioRepository
    {
        private readonly SenaiNotesContext _context;

        public NovoUsuarioRepository(SenaiNotesContext context)
        {
            _context = context;
        }

        public async Task Atualizar(int id, NovoUsuarioDto usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(id);

            if (usuarioExistente == null)
                throw new InvalidOperationException("Usuário não encontrado!");

            await _context.SaveChangesAsync();
        }

        public async Task Cadastrar(NovoUsuarioDto usuario)
        {
            var passwordService = new PasswordService();

            var novoUsuario = new Usuario
            {
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email,
                Senha = usuario.Senha,
            };

            novoUsuario.Senha = passwordService.HashPassword(novoUsuario);

            await _context.Usuarios.AddAsync(novoUsuario);
            await _context.SaveChangesAsync();

        }

        public async Task<Usuario?> Login(string email, string senha)
        {
            try
            {
                var usuarioEncontrado = await _context.Usuarios
                    .FirstOrDefaultAsync(c => c.Email == email);

                if (usuarioEncontrado == null)
                    return null;

                var passwordService = new PasswordService();

                var resultado = passwordService.VerificarSenha(usuarioEncontrado, senha);

                return resultado ? usuarioEncontrado : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
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
