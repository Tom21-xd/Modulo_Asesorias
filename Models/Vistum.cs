using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Vistum
{
    public int IdVista { get; set; }

    public string ControladorVista { get; set; } = null!;

    public string AccionVista { get; set; } = null!;

    public sbyte EstadoVista { get; set; }

    public int FkIdPermiso { get; set; }

    public virtual Permiso FkIdPermisoNavigation { get; set; } = null!;
}
