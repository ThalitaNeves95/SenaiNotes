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

        public List<Nota> BuscarNotaPorTitulo(string titulo)
        {
            //Where - traz todos que atendem uma condicao
            //var listaClientes = _context.Clientes.Where(c => c.NomeCompleto == nome).ToList();
            var listarNotas = _context.Notas.Where(c => c.Titulo.Contains(titulo)).ToList();

            return listarNotas;
        }

        public void Atualizar(int id, Nota nota)
        {

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

            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var notaEncontrada = _context.Notas.Find(id);

            if (notaEncontrada == null)
            {
                throw new Exception();
            }

            _context.Notas.Remove(notaEncontrada);

            _context.SaveChanges();
        }

        public List<Nota> ListarTodos()
        {
            return _context.Notas.ToList();
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
