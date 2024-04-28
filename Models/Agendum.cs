using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Agendum
{
    public int IdAgenda { get; set; }

    public int FkIdProfesor { get; set; }

    public DateOnly FechaAgenda { get; set; }

    public string DiaAgenda { get; set; } = null!;

    public TimeOnly HorainicioAgenda { get; set; }

    public TimeOnly HorafinAgenda { get; set; }

    public int NumeroAgenda { get; set; }

    public int FkIdSala { get; set; }

    public int? FkIdEstudiante { get; set; }

    public virtual Usuario? FkIdEstudianteNavigation { get; set; }

    public virtual Usuario FkIdProfesorNavigation { get; set; } = null!;

    public virtual Sala FkIdSalaNavigation { get; set; } = null!;
}
