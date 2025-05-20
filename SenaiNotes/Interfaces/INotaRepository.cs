using SenaiNotes.Models;

namespace APISenaiNotes.Interfaces
{
    public interface INotaRepository
    {
        
        List<Nota> ListarTodos();

        Nota BuscarPorId(int id);

        List<Nota> BuscarNotaPorTitulo(string titulo);

        void Cadastrar(Tag tag);

        void Atualizar(int id, Tag tag);

        void Deletar(int id);

        Task ArquivarNota(int id);
    }
}
