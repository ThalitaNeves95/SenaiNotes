using APISenaiNotes.Interfaces;
using APISenaiNotes.Models;
using Microsoft.EntityFrameworkCore;
using APISenaiNotes.Context;
using APISenaiNotes.ViewModels;

namespace APISenaiNotes.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly SenaiNotesContext _context;
        public TagRepository(SenaiNotesContext context)
        {
            _context = context;
        }

        public async Task<List<ListarTagViewModel>> ListarTodos()
        {
            var tags = _context.Tags
                 .Include(n => n.Nota)
                 .Select(n => new ListarTagViewModel
                 {
                     TagId = n.TagId,
                     Nome = n.Nome,
                 });

            return await tags.ToListAsync();
        }

        public async Task<Tag?> BuscarPorNomeeUsuario(string nome)
        {
            return await _context.Tags
            .FirstOrDefaultAsync(t => t.Nome == nome);
        }

        public async Task Cadastrar(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(int id, Tag tag)
        {
            var tagBuscada = await _context.Tags.FindAsync(id);
            if (tagBuscada != null)
            {
                tagBuscada.Nome = tag.Nome;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Deletar(int id)
        {
            var tagBuscada = await _context.Tags.FindAsync(id);

            if (tagBuscada != null)
            {
                _context.Tags.Remove(tagBuscada);
                await _context.SaveChangesAsync();
            }
        }
    }
}
