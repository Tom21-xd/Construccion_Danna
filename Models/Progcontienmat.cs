using System;
using System.Collections.Generic;

namespace Construccion_Danna.Models;

public partial class Progcontienmat
{
    public int ProgId { get; set; }

    public int AsiId { get; set; }

    public DateOnly FechaAgregacion { get; set; }

    public virtual Asignatura Asi { get; set; } = null!;

    public virtual Programa Prog { get; set; } = null!;
}
