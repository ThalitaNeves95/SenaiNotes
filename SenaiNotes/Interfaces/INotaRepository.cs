using APISenaiNotes.DTO;
using APISenaiNotes.ViewModels;
using APISenaiNotes.Models;

namespace APISenaiNotes.Interfaces
{
    public interface INotaRepository
    {
        Task<List<ListarNotaViewModel>> ListarTodos();

        Task<Nota?> BuscarPorId(int id);

        Task<List<Nota>> BuscarNotaPorTexto(string titulo);

        Task Cadastrar(CadastrarNotaDto nota);

        Task Atualizar(int id, CadastrarNotaDto nota);

        Task Deletar(int id);

        Task ArquivarNota(int id);
    }
}
