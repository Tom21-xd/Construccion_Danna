using System;
using System.Collections.Generic;

namespace Construccion_Danna.Models;

public partial class Sala
{
    public int IdSala { get; set; }

    public string BloqueSala { get; set; } = null!;

    public int NumeroSala { get; set; }

    public int PkAsesoria { get; set; }

    public virtual Asesorium PkAsesoriaNavigation { get; set; } = null!;
}
