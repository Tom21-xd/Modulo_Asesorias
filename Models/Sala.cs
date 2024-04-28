using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Sala
{
    public int IdSala { get; set; }

    public string BloqueSala { get; set; } = null!;

    public string NumeroSala { get; set; } = null!;

    public virtual ICollection<Agendum> Agenda { get; set; } = new List<Agendum>();

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();
}
