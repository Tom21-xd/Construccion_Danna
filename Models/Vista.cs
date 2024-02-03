using System;
using System.Collections.Generic;

namespace Construccion_Danna.Models;

public partial class Vista
{
    public int VisId { get; set; }

    public string VisControlador { get; set; } = null!;

    public string VisAccion { get; set; } = null!;

    public sbyte VisEstado { get; set; }

    public int FkpermId { get; set; }

    public virtual Permiso Fkperm { get; set; } = null!;
}
