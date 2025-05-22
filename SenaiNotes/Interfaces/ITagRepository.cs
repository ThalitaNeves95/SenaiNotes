using APISenaiNotes.Models;

namespace APISenaiNotes.Interfaces
{
    public interface ITagRepository
    {
        Task<List<Tag>> ListarTodos();

        Task<Tag?> BuscarPorNomeeUsuario(string nome);

        Task Cadastrar(Tag tag);

        Task Atualizar(int id, Tag tag);

        Task Deletar(int id);
    }
}
