using SenaiNotes.Models;

namespace APISenaiNotes.Interfaces
{
    public interface INotaRepository
    {
        // Read - Ler
        List<Nota> ListarTodos();

        Nota BuscarPorId(int id);

        // Create
        void Cadastrar(Nota nota);

        // Update
        void Atualizar(int id, Nota nota);

        // Delete
        void Deletar(int id);

        Task ArquivarNota(int id, bool? arquivada);
    }
}
