using APISenaiNotes.DTO;
using SenaiNotes.Models;

namespace APISenaiNotes.Interfaces
{
    public interface INotaRepository
    {
        Task<List<Nota>> ListarTodos();

        Task<Nota?> BuscarPorId(int id);

        Task<List<Nota>> BuscarNotaPorTitulo(string titulo);

        Task Cadastrar(CadastrarNotaDto nota);

        Task Atualizar(int id, CadastrarNotaDto nota);

        Task Deletar(int id);

        Task ArquivarNota(int id);
    }
}
