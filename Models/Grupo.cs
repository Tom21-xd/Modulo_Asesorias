using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Grupo
{
    public int IdGrupo { get; set; }

    public sbyte EstadoGrupo { get; set; }

    public int NumeroGrupo { get; set; }

    public int FkIdAsignatura { get; set; }

    public int FkIdSala { get; set; }

    public virtual Asignatura FkIdAsignaturaNavigation { get; set; } = null!;

    public virtual Sala FkIdSalaNavigation { get; set; } = null!;

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
