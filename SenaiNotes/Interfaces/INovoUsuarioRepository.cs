using APISenaiNotes.Models;

namespace APISenaiNotes.Interfaces
{
    public interface INovoUsuarioRepository
    {

        List<Usuario> ListarTodos();

        Usuario? BuscarPorId(int id);

        List<Usuario> BuscarClientePorNome(string nome);

        // Create
        void Cadastrar(Usuario usuario);

        // Update
        void Atualizar(int id, Usuario cliente);

        // Delete
        void Deletar(int id);
    }
}
