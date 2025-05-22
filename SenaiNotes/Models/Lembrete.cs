using System;
using System.Collections.Generic;

namespace APISenaiNotes.Models;

public partial class Lembrete
{
    public int LembreteId { get; set; }

    public int? NotaId { get; set; }

    public string? Descricao { get; set; }

    public DateTime DataLembrete { get; set; }

    public bool? Ativo { get; set; }

    public virtual Nota? Nota { get; set; }
}
