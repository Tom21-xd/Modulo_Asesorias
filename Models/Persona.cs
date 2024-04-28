using System;
using System.Collections.Generic;

namespace Modulo_Asesorias.Models;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string Nombre1Persona { get; set; } = null!;

    public string? Nombre2Persona { get; set; }

    public string Apellido1Persona { get; set; } = null!;

    public string? Apellido2Persona { get; set; }

    public int FkIdTipodocumento { get; set; }

    public virtual Tipodocumento FkIdTipodocumentoNavigation { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
