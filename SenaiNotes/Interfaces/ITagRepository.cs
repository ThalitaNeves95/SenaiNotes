using SenaiNotes.Models;

namespace APISenaiNotes.Interfaces
{
    public interface ITagRepository
    {
        List<Tag> ListarTodos();

        Tag BuscarPorId(int id);

        List<Tag> BuscarTagPorTitulo(string titulo);

        void Cadastrar(Nota nota);

        void Atualizar(int id, Nota nota);

        void Deletar(int id);
    }
}
