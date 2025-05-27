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
            _context = context;
            _tagRepository = tagRepository;
        }

        public async Task Atualizar(int id, CadastrarNotaDto nota)
        {
            var notaEncontrada = await _context.Notas.FindAsync(id);

            if (notaEncontrada == null)
            {
                throw new ArgumentNullException();
            }

            notaEncontrada.Titulo = nota.Titulo;
            notaEncontrada.Imagem = nota.Imagem;
            notaEncontrada.Conteudo = nota.Conteudo;

            await _context.SaveChangesAsync();
        }

        public async Task<List<CadastrarNotaDto>> BuscarNotaPorNomeAsync(string nota)
        {
            return await _context.Notas
                .Where(n => n.Titulo.Contains(nota))
                .Select(n => new CadastrarNotaDto
                {
                    Titulo = n.Titulo,
                    Imagem = n.Imagem,
                    Conteudo = n.Conteudo,
                    Tags = n.Tags.Select(t => t.Nome).ToList()
                })
                .ToListAsync();
        }

        public async Task<CadastrarNotaSemImagemDto> CadastrarNotaSemImagemDto(CadastrarNotaSemImagemDto notaDto)
        {
            // 1 - Percorrer a Lista de Tags
            // 1.1 - Essa Tag já existe?
            //1.2 - Pegar o ID dela
            // 1.3 - Se não existe, cadastrar a tag e pegar ID dela
            List<Tag> tags = new List<Tag>();

            foreach (var item in notaDto.Tags)
            {
                var tagEncontrada = await _tagRepository.BuscarPorNomeeUsuario(item);

                if (tagEncontrada == null)
                {
                    tagEncontrada = new Tag
                    {
                        Nome = item,
                    };
                    _context.Tags.Add(tagEncontrada);
                    _context.SaveChanges();
                }

                tags.Add(tagEncontrada);

            }

            var novaNota = new Nota
            {
                Titulo = notaDto.Titulo,
                Imagem = notaDto.Imagem,
                Conteudo = notaDto.Conteudo,
                UsuarioId = notaDto.UsuarioId,
                Arquivada = false,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Tags = tags
            };

            _context.Notas.Add(novaNota);
            await _context.SaveChangesAsync();

            return notaDto;
        }

        public async Task<CadastrarNotaDto> CadastrarNotaDto(CadastrarNotaDto notaDto)
        {
            // 1 - Percorrer a Lista de Tags
            // 1.1 - Essa Tag já existe?
            //1.2 - Pegar o ID dela
            // 1.3 - Se não existe, cadastrar a tag e pegar ID dela
            List<Tag> tags = new List<Tag>();

            foreach (var item in notaDto.Tags)
            {
                var tagEncontrada = await _tagRepository.BuscarPorNomeeUsuario(item);

                if (tagEncontrada == null)
                {
                    tagEncontrada = new Tag
                    {
                        Nome = item,
                    };
                    _context.Tags.Add(tagEncontrada);
                    _context.SaveChanges();
                }

                tags.Add(tagEncontrada);
                
            }

            var novaNota = new Nota
            {
                Titulo = notaDto.Titulo,
                Imagem = notaDto.Imagem,
                Conteudo = notaDto.Conteudo,
                UsuarioId = notaDto.UsuarioId,
                Arquivada = false,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Tags = tags
            };

            _context.Notas.Add(novaNota);
            await _context.SaveChangesAsync();

            return notaDto;
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
                    UsuarioId = n.UsuarioId,
                    Arquivada = n.Arquivada,
                    DataCriacao = n.DataCriacao,
                    DataAtualizacao = n.DataAtualizacao,
                    Tags = n.Tags.Select(t => new ListarTagViewModel
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
    }
}
