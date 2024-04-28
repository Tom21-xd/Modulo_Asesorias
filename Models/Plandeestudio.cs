using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Plandeestudio
{
    public int IdPlandeestudio { get; set; }

    public string NombrePlandeestudio { get; set; } = null!;

    public sbyte EstadoPlandeestudio { get; set; }

    public int FkIdPrograma { get; set; }

    public virtual ICollection<Asignaturaplandeestudio> Asignaturaplandeestudios { get; set; } = new List<Asignaturaplandeestudio>();

    public virtual Programa FkIdProgramaNavigation { get; set; } = null!;
}
