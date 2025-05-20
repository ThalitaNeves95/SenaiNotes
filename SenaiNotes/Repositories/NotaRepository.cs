using APISenaiNotes.Interfaces;
using Microsoft.EntityFrameworkCore;
using SenaiNotes.Context;
using SenaiNotes.Models;

namespace APISenaiNotes.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly SenaiNotesContext _context;

        public NotaRepository(SenaiNotesContext context)
        {
            _context = context;
        }

        public async Task<Nota?> BuscarPorId(int id)
        {
            return await _context.Notas.FirstOrDefaultAsync(p => p.NotaId == id);
        }

        public async Task<List<Nota>> BuscarNotaPorTitulo(string titulo)
        {
            return await _context.Notas.Where(n => n.Titulo.Contains(titulo)).ToListAsync();
        }

        public async Task Atualizar(int id, Nota nota)
        {
            var NotaEncontrada = await _context.Notas.FindAsync(id);

            if (NotaEncontrada == null)
            {
                throw new Exception();
            }

            await _context.SaveChangesAsync();
        }

        public async Task Cadastrar(Nota nota)
        {
            await _context.Notas.AddAsync(nota);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(int id)
        {
            var notaEncontrada = await _context.Notas.FindAsync(id);

            if (notaEncontrada == null)
            {
                throw new Exception();
            }

            _context.Notas.Remove(notaEncontrada);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Nota>> ListarTodos()
        {
            return await _context.Notas.ToListAsync();
        }

        public async Task ArquivarNota(int id)
        {
            var notaEncontrada = await _context.Notas.FindAsync(id);
            if (notaEncontrada != null)
            {
                notaEncontrada.Arquivada = !notaEncontrada.Arquivada;
                await _context.SaveChangesAsync();
            }
        }
    }
}
