using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Tipodocumento
{
    public int IdTipodocumento { get; set; }

    public string NombreTipodocumento { get; set; } = null!;

    public sbyte EstadoTipodocumento { get; set; }

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
