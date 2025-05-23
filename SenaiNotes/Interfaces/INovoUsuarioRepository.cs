using APISenaiNotes.DTO;
using APISenaiNotes.Models;

namespace APISenaiNotes.Interfaces
{
    public interface INovoUsuarioRepository
    {

        Task<List<Usuario>> ListarTodos();

        //Task<Usuario?> BuscarPorEmailSenha(string email, string senha);

        Task Cadastrar(NovoUsuarioDto usuario);

        Task<Usuario?> Login(string email, string senha);

        Task Atualizar(int id, Usuario usuario);
        
        Task Deletar(int id);
    }
}
