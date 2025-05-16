using APISenaiNotes.Interfaces;
using SenaiNotes.Context;
using SenaiNotes.Models;
using static APISenaiNotes.Repositories.NotaRepository;

namespace APISenaiNotes.Repositories
{
    public class NotaRepository : INotaRepository
    {
        // Repository - Métodos que acessam o Banco de Dados
            // Injetar o Context - Banco de Dados
            // Injeção de dependência
            // Variavel privada, porque só vai ser usada nessa classe.
            private readonly SenaiNotesContext _context;

            // Método Construtor - ctor
            // Quando criar um objeto o que eu preciso ter?
            // É semelhante ao new()
            public NotaRepository(SenaiNotesContext context)
            {
                _context = context;
            }

            public Nota? BuscarPorId(int id)
            {
                return _context.Notas.FirstOrDefault(p => p.NotaId == id);
            }

            public void Atualizar(int id, Nota nota)
            {
                // Encontrar o produto a ser atualizado
                var NotaEncontrada = _context.Notas.Find(id);

                if (NotaEncontrada == null)
                {
                    throw new Exception();
                }

                _context.SaveChanges();
            }

            public void Cadastrar(Nota nota)
            {
                _context.Notas.Add(nota);
                
                // 2 - Salvo a Alteração
                _context.SaveChanges();
            }

            public void Deletar(int id)
            {
                // 1 - encontrar o que eu quero excluir
                var notaEncontrada = _context.Notas.Find(id); // Find - Procura apenas pela chave primaria

                // Tratamento de erro
                if (notaEncontrada == null)
                {
                    throw new Exception();
                }

                // 2 - Caso eu enconte o produto, removo ele
                _context.Notas.Remove(notaEncontrada);

                // 3 - Salvo as alteracoes
                _context.SaveChanges();
            }

            public List<Nota> ListarTodos()
            {
                return _context.Notas.ToList();
            }

        public async Task ArquivarNota(int id, bool? arquivada) 
        {

            var notaEncontrada = await _context.Notas.FindAsync(id);
            if (notaEncontrada != null)
            {
                notaEncontrada.Arquivada = arquivada ?? true; 
                await _context.SaveChangesAsync(); 
            }
            
        }

        //public void DesarquivarNota(int id, bool? arquivado)
        //{
        //    using (var context = new SenaiNotesContext())
        //    {
        //        var registro = context.Notas.Find(id);
        //        if (registro != null)
        //        {
        //            registro.Arquivada = false;
        //            context.SaveChanges();
        //        }
        //    }
        //}
    }
}
