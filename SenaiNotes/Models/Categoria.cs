using System;
using System.Collections.Generic;

namespace SenaiNotes.Models;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
