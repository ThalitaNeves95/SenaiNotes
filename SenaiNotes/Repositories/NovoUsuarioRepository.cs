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

        public async Task Atualizar(int id, Usuario usuario)
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

        //public async Task Cadastrar(Usuario usuario)
        //{
        //    await _context.Usuarios.AddAsync(usuario);

        //    await _context.SaveChangesAsync();
        //}

        public async Task<Usuario?> Login(string email, string senha)
        {
            try
            {
                // Busca assíncrona do cliente por email
                var usuarioEncontrado = await _context.Usuarios
                    .FirstOrDefaultAsync(c => c.Email == email);

                // Caso não encontre, retorna nulo
                if (usuarioEncontrado == null)
                    return null;

                var passwordService = new PasswordService();

                // Verificação da senha (assumindo que é uma operação síncrona)
                var resultado = passwordService.VerificarSenha(usuarioEncontrado, senha);

                return resultado ? usuarioEncontrado : null;
            }
            catch (Exception ex)
            {
                // Log do erro (opcional)
                // _logger.LogError(ex, "Erro ao buscar cliente por email e senha");
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
