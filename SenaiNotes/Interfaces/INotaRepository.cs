using APISenaiNotes.DTO;
using APISenaiNotes.ViewModels;
using APISenaiNotes.Models;

namespace APISenaiNotes.Interfaces
{
    public interface INotaRepository
    {
        Task<List<ListarNotaViewModel>> ListarTodos();

        Task <CadastrarNotaDto> CadastrarNotaDto(CadastrarNotaDto nota);

        Task<List<CadastrarNotaDto>> BuscarNotaPorNomeAsync(string nota);

        Task Atualizar(int id, CadastrarNotaDto nota);

        Task Deletar(int id);

        Task ArquivarNota(int id);
    }
}
