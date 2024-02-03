using System;
using System.Collections.Generic;

namespace Construccion_Danna.Models;

public partial class Permiso
{
    public int PermId { get; set; }

    public string PermPermiso { get; set; } = null!;

    public sbyte PermEstado { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    public virtual ICollection<Vista> Vista { get; set; } = new List<Vista>();
}
