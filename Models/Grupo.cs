using System;
using System.Collections.Generic;

namespace Construccion_Danna.Models;

public partial class Grupo
{
    public int GrupId { get; set; }

    public string GrupNumeroGrupo { get; set; } = null!;

    public sbyte GrupEstado { get; set; }

    public int FkAsiId { get; set; }

    public virtual Asignatura FkAsi { get; set; } = null!;
}
