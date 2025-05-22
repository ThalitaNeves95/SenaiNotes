using APISenaiNotes.Interfaces;
using APISenaiNotes.Models;
using Microsoft.EntityFrameworkCore;
using APISenaiNotes.Context;

namespace APISenaiNotes.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly SenaiNotesContext _context;
        public TagRepository(SenaiNotesContext context)
        {
            _context = context;
        }

        public async Task<List<Tag>> ListarTodos()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag?> BuscarPorId(int id)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.TagId == id);
        }

        public async Task<List<Tag>> BuscarTagPorTitulo(string titulo)
        {
            return await _context.Tags
                        .Where(t => t.Nome.Contains(titulo))
                        .ToListAsync();
        }

        public async Task Cadastrar(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(int id, Tag tag)
        {
            var tagBuscada = await BuscarPorId(id);
            if (tagBuscada != null)
            {
                tagBuscada.Nome = tag.Nome;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Deletar(int id)
        {
            var tagBuscada = await BuscarPorId(id);
            if (tagBuscada != null)
            {
                _context.Tags.Remove(tagBuscada);
                await _context.SaveChangesAsync();
            }
        }
    }
}
