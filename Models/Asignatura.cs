using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Asignatura
{
    public int IdAsignatura { get; set; }

    public string NombreAsignatura { get; set; } = null!;

    public sbyte EstadoAsignatura { get; set; }

    public virtual ICollection<Asignaturaplandeestudio> Asignaturaplandeestudios { get; set; } = new List<Asignaturaplandeestudio>();

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    public virtual ICollection<Impartir> Impartirs { get; set; } = new List<Impartir>();
}
