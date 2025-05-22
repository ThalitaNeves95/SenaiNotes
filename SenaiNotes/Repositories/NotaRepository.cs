using APISenaiNotes.DTO;
using APISenaiNotes.Interfaces;
using APISenaiNotes.Models;
using APISenaiNotes.ViewModels;
using Microsoft.EntityFrameworkCore;
using APISenaiNotes.Context;

namespace APISenaiNotes.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly ITagRepository _tagRepository;

        private readonly SenaiNotesContext _context;

        public NotaRepository(SenaiNotesContext context, ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
            _context = context;
        }

        public async Task<Nota?> BuscarPorUsuarioeId(int id)
        {
            var tags = await _context.Notas.FirstOrDefaultAsync(p => p.NotaId == id);
        }

        public async Task<List<Nota>> BuscarNotaPorTexto(string titulo)
        {
            return await _context.Notas.Where(n => n.Titulo.Contains(titulo)).ToListAsync();
        }

        public async Task Atualizar(int id, CadastrarNotaDto nota)
        {
            var NotaEncontrada = await _context.Notas.FindAsync(id);

            if (NotaEncontrada == null)
            {
                throw new Exception();
            }

            await _context.SaveChangesAsync();
        }

        public async Task Cadastrar(CadastrarNotaDto notaDto)
        {
            // 1 - Percorrer a Lista de Tags
            // 1.1 - Essa Tag já existe?
            //1.2 - Pegar o ID dela
            // 1.3 - Se não existe, cadastrar a tag e pegar ID dela
            List<int>idTags = new List<int>();
            foreach (var tag in notaDto.Tags)
            {
                var tagEncontrada = await _tagRepository.BuscarTagPorTitulo(tag.Nome, item);

                if (tagEncontrada == null)
                {
                    idTags.Add(tagEncontrada.TagId);
                }
                else
                {
                    await _tagRepository.Cadastrar(tag);
                    idTags.Add(tag.TagId);
                }
            }
            await _context.Notas.AddAsync();
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

        public async Task<List<ListarNotaViewModel>> ListarTodos()
        {
            var anotacoes = _context.Notas
                .Include(n => n.Tags)
                .Select(n => new ListarNotaViewModel
                {
                    NotaId = n.NotaId,
                    Titulo = n.Titulo,
                    Imagem = n.Imagem,
                    Conteudo = n.Conteudo,
                    Arquivada = n.Arquivada,
                    DataCriacao = n.DataCriacao,
                    DataAtualizacao = n.DataAtualizacao,
                    Tags = n.Tags.Select(t => new Tag
                    {
                        TagId = t.TagId,
                        Nome = t.Nome
                    }).ToList()
                });

            return await anotacoes.ToListAsync();
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

        public Task<Nota?> BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
