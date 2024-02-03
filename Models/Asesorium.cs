using System;
using System.Collections.Generic;

namespace Construccion_Danna.Models;

public partial class Asesorium
{
    public int AseId { get; set; }

    public string AseDia { get; set; } = null!;

    public TimeOnly AseHoraInicio { get; set; }

    public TimeOnly AseHoraFin { get; set; }

    public int AseNumero { get; set; }

    public int FkUsuId { get; set; }

    public virtual Usuario FkUsu { get; set; } = null!;

    public virtual ICollection<Sala> Salas { get; set; } = new List<Sala>();
}
