using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Impartir
{
    public int FkIdUsuario { get; set; }

    public int FkIdAsignatura { get; set; }

    public sbyte? EstadoImpartir { get; set; }

    public virtual Asignatura FkIdAsignaturaNavigation { get; set; } = null!;

    public virtual Usuario FkIdUsuarioNavigation { get; set; } = null!;
}
