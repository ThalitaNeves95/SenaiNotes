using APISenaiNotes.DTO;
using APISenaiNotes.Models;

namespace APISenaiNotes.Interfaces
{
    public interface INovoUsuarioRepository
    {

        Task<List<Usuario>> ListarTodos();

        Task Cadastrar(NovoUsuarioDto usuario);

        Task<Usuario?> Login(string email, string senha);

        Task Atualizar(int id, NovoUsuarioDto usuario);
        
        Task Deletar(int id);
    }
}
