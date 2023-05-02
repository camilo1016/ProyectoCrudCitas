namespace BackEndApi.DTOs
{
    public class PacienteDTO
    {
        public int IdPaciente { get; set; }

        public string? NombreCompleto { get; set; }

        public int? IdTipoId { get; set; }
        public string? NombreTipoId { get; set; }

        public int? NumeroId { get; set; }

        public string? Consulta { get; set; }

        public string? FechaConsulta { get; set; }
    }
}
