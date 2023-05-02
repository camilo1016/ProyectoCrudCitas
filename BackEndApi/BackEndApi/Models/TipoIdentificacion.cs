using System;
using System.Collections.Generic;

namespace BackEndApi.Models;

public partial class TipoIdentificacion
{
    public int IdTipoId { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Paciente> Pacientes { get; } = new List<Paciente>();
}
