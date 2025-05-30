﻿using APISenaiNotes.Models;


namespace APISenaiNotes.ViewModels
{
    public class ListarNotaViewModel
    {
        public int NotaId { get; set; }

        public int UsuarioId { get; set; }

        public string? Titulo { get; set; }

        public string? Imagem { get; set; }

        public string? Conteudo { get; set; }

        public bool? Arquivada { get; set; }

        public DateTime? DataCriacao { get; set; }

        public DateTime? DataAtualizacao { get; set; }
        public virtual ICollection<ListarTagViewModel> Tags { get; set; } = new List<ListarTagViewModel>();
    }
}
