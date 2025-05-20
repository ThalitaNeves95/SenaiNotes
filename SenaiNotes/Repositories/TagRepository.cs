using APISenaiNotes.Interfaces;
using SenaiNotes.Context;
using SenaiNotes.Models;

namespace APISenaiNotes.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly SenaiNotesContext _context;
        public TagRepository(SenaiNotesContext context)
        {
            _context = context;
        }
        public List<Tag> ListarTodos()
        {
            return _context.Tags.ToList();
        }
        public Tag? BuscarPorId(int id)
        {
            return _context.Tags.FirstOrDefault(t => t.TagId == id);
        }
        public List<Tag> BuscarTagPorTitulo(string titulo)
        {
            return _context.Tags.Where(t => t.Nome.Contains(titulo)).ToList();
        }
        public void Cadastrar(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }
        public void Atualizar(int id, Tag tag)
        {
            var tagBuscada = BuscarPorId(id);
            if (tagBuscada != null)
            {
                tagBuscada.Nome = tag.Nome;
                _context.SaveChanges();
            }
        }
        public void Deletar(int id)
        {
            var tagBuscada = BuscarPorId(id);
            if (tagBuscada != null)
            {
                _context.Tags.Remove(tagBuscada);
                _context.SaveChanges();
            }
        }
    }
}
