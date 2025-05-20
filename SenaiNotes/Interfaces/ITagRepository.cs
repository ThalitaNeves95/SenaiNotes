using SenaiNotes.Models;

namespace APISenaiNotes.Interfaces
{
    public interface ITagRepository
    {
        Task<List<Tag>> ListarTodos();

        Task<Tag?> BuscarPorId(int id);

        Task<List<Tag>> BuscarTagPorTitulo(string titulo);

        Task Cadastrar(Tag tag);

        Task Atualizar(int id, Tag tag);

        Task Deletar(int id);
    }
}
