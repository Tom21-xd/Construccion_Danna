using System;
using System.Collections.Generic;

namespace Construccion_Danna.Models;

public partial class Asignatura
{
    public int AsiId { get; set; }

    public string AsiNombre { get; set; } = null!;

    public sbyte AsiEstado { get; set; }

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    public virtual ICollection<Progcontienmat> Progcontienmats { get; set; } = new List<Progcontienmat>();

    public virtual ICollection<Usuario> FkUsus { get; set; } = new List<Usuario>();
}
