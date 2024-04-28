using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Matricula
{
    public int FkIdGrupo { get; set; }

    public int FkIdUsuario { get; set; }

    public DateOnly FechaMatricula { get; set; }

    public virtual Grupo FkIdGrupoNavigation { get; set; } = null!;

    public virtual Usuario FkIdUsuarioNavigation { get; set; } = null!;
}
