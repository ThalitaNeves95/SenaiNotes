using SenaiNotes.Models;

namespace APISenaiNotes.Interfaces
{
    public interface INotaRepository
    {
        
        List<Nota> ListarTodos();

        Nota BuscarPorId(int id);

        
        void Cadastrar(Nota nota);

        
        void Atualizar(int id, Nota nota);

        
        void Deletar(int id);

        Task ArquivarNota(int id, bool? arquivada);
    }
}
