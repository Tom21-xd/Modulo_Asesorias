using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Facultad
{
    public int IdFacultad { get; set; }

    public string NombreFacultad { get; set; } = null!;

    public sbyte EstadoFacultad { get; set; }

    public virtual ICollection<Programa> Programas { get; set; } = new List<Programa>();
}
