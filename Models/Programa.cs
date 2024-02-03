using System;
using System.Collections.Generic;

namespace Construccion_Danna.Models;

public partial class Programa
{
    public int ProgId { get; set; }

    public string ProgNombre { get; set; } = null!;

    public sbyte ProgEstado { get; set; }

    public virtual ICollection<Progcontienmat> Progcontienmats { get; set; } = new List<Progcontienmat>();
}
