
using BackEndApi.Models;

namespace BackEndApi.Services.Contrato
{
    public interface ITipoIdentificacionService
    {
        Task<List<TipoIdentificacion>> GetList();
    }
}
