using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Programa
{
    public int IdPrograma { get; set; }

    public string NombrePrograma { get; set; } = null!;

    public string EstadoPrograma { get; set; } = null!;

    public int FkIdFacultad { get; set; }

    public virtual Facultad FkIdFacultadNavigation { get; set; } = null!;

    public virtual ICollection<Plandeestudio> Plandeestudios { get; set; } = new List<Plandeestudio>();
}
