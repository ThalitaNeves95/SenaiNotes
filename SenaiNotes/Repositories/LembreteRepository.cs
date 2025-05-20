using APISenaiNotes.Interfaces;
using SenaiNotes.Context;
using SenaiNotes.Models;
using System.Collections.Generic;
using System.Linq;

namespace APISenaiNotes.Repositories
{
    public class LembreteRepository : ILembreterRepository
    {
        private readonly SenaiNotesContext _context;

        public LembreteRepository(SenaiNotesContext context)
        {
            _context = context;
        }
        public void Cadastrar(Lembrete lembrete)
        {
            _context.Lembretes.Add(lembrete);
            _context.SaveChanges();

        }
        public List<Lembrete> ListarTodos()
        {
            return _context.Lembretes.ToList();
        }
        public Lembrete BuscarPorId(int id)
        {
            return _context.Lembretes.FirstOrDefault(l => l.LembreteId == id);
        }
        public void Atualizar(int id, Lembrete lembrete)
        {
            Lembrete lembreteBuscado = _context.Lembretes.Find(id);
            if (lembreteBuscado != null)
            {
                lembreteBuscado.Descricao = lembrete.Descricao;
                lembreteBuscado.DataLembrete = lembrete.DataLembrete;
                lembreteBuscado.Ativo = lembrete.Ativo;
                _context.Lembretes.Update(lembreteBuscado);
                _context.SaveChanges();
            }
        }
        public void Deletar(int id)
        {
            Lembrete lembreteBuscado = _context.Lembretes.Find(id);
            if (lembreteBuscado != null)
            {
                _context.Lembretes.Remove(lembreteBuscado);
                _context.SaveChanges();
            }
        }

    }
    
}
