using APISenaiNotes.Models;
using System.Collections.Generic;

namespace APISenaiNotes.Interfaces
{
    public interface ILembreterRepository
    {
        void Cadastrar(Lembrete lembrete);
        List<Lembrete> ListarTodos();
        Lembrete BuscarPorId(int id);
        void Atualizar(int id, Lembrete lembrete);
        void Deletar(int id);
    }
}
