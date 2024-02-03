using System;
using System.Collections.Generic;

namespace Construccion_Danna.Models;

public partial class Peragendaase
{
    public int AseAsesoria { get; set; }

    public DateOnly FechaAgendacion { get; set; }

    public int FkUsuId { get; set; }

    public virtual Asesorium AseAsesoriaNavigation { get; set; } = null!;

    public virtual Usuario FkUsu { get; set; } = null!;
}
