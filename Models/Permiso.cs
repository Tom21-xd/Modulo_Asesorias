using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Permiso
{
    public int IdPermiso { get; set; }

    public string NombrePermiso { get; set; } = null!;

    public sbyte EstadoPermiso { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    public virtual ICollection<Vistum> Vista { get; set; } = new List<Vistum>();
}
