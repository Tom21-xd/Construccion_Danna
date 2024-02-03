using System;
using System.Collections.Generic;

namespace Construccion_Danna.Models;

public partial class Usuario
{
    public int UsuId { get; set; }

    public string UsuCorreo { get; set; } = null!;

    public string UsuToken { get; set; } = null!;

    public sbyte UsuEstado { get; set; }

    public int FkpermisoId { get; set; }

    public int Fkpersona { get; set; }

    public Persona Persona { get; set; }

    public virtual ICollection<Asesorium> Asesoria { get; set; } = new List<Asesorium>();

    public virtual Permiso Fkpermiso { get; set; } = null!;

    public virtual Persona FkpersonaNavigation { get; set; } = null!;

    public virtual ICollection<Asignatura> FkAsis { get; set; } = new List<Asignatura>();
}
