using System;
using System.Collections.Generic;

namespace Construccion_Danna.Models;

public partial class Tipodocumento
{
    public int TipdocId { get; set; }

    public string TipdocTipoDocumento { get; set; } = null!;

    public sbyte TipdocEstado { get; set; }

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
