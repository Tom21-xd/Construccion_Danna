using System;
using System.Collections.Generic;

namespace Construccion_Danna.Models;

public partial class Persona
{
    public int PerCodigo { get; set; }

    public string PerPrimerNombre { get; set; } = null!;

    public string? PerSegundoNombre { get; set; }

    public string PerPrimerApellido { get; set; } = null!;

    public string? PerSegundoApellido { get; set; }

    public sbyte PerEstado { get; set; }

    public int FktipdocId { get; set; }

    public virtual Tipodocumento Fktipdoc { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
