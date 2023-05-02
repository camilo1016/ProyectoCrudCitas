
using AutoMapper;
using BackEndApi.DTOs;
using BackEndApi.Models;
using System.Globalization;

namespace BackEndApi.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region TipoIdentificacion
            CreateMap<TipoIdentificacion, TipoIdentificacionDTO>().ReverseMap();

            #endregion

            #region Paciente
            CreateMap<Paciente, PacienteDTO>()
                .ForMember(destino =>
                destino.NombreTipoId,
                opt => opt.MapFrom(origen => origen.IdTipo.Nombre)
                )
                .ForMember(destino =>
                destino.FechaConsulta,
                opt => opt.MapFrom(origen => origen.FechaConsulta.Value.ToString("dd/MM/yyyy"))
                );
            

            CreateMap<PacienteDTO, Paciente>()
                .ForMember(destino =>
                    destino.IdTipo,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.FechaConsulta,
                    opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaConsulta,"dd/MM/yyyy", CultureInfo.InvariantCulture))
                );

            #endregion

        }
    }
}
