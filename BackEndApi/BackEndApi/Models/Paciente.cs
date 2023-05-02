using System;
using System.Collections.Generic;

namespace BackEndApi.Models;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public string? NombreCompleto { get; set; }

    public int? IdTipoId { get; set; }

    public int? NumeroId { get; set; }

    public string? Consulta { get; set; }

    public DateTime? FechaConsulta { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Status { get; set; }

    public virtual TipoIdentificacion? IdTipo { get; set; }
}
