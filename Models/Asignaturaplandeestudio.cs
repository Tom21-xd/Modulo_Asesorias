using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Asignaturaplandeestudio
{
    public int FkIdAsignatura { get; set; }

    public int FkIdPlandeestudio { get; set; }

    public int SemestreAsignaturaplandeestudio { get; set; }

    public virtual Asignatura FkIdAsignaturaNavigation { get; set; } = null!;

    public virtual Plandeestudio FkIdPlandeestudioNavigation { get; set; } = null!;
}
