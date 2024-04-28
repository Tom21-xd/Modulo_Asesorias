using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string CorreoUsuario { get; set; } = null!;

    public string ContraseniaUsuario { get; set; } = null!;

    public sbyte EstadoUsuario { get; set; }

    public int FkIdPersona { get; set; }

    public int FkIdPermiso { get; set; }

    public virtual ICollection<Agendum> AgendumFkIdEstudianteNavigations { get; set; } = new List<Agendum>();

    public virtual ICollection<Agendum> AgendumFkIdProfesorNavigations { get; set; } = new List<Agendum>();

    public virtual Permiso FkIdPermisoNavigation { get; set; } = null!;

    public virtual Persona FkIdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<Impartir> Impartirs { get; set; } = new List<Impartir>();

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
