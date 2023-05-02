
using Microsoft.EntityFrameworkCore;
using BackEndApi.Models;
using BackEndApi.Services.Contrato;

namespace BackEndApi.Services.Implementacion
{
    public class TipoIdentificacionService : ITipoIdentificacionService
    {
        private DbpacienteContext _dbContext;

        public TipoIdentificacionService(DbpacienteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TipoIdentificacion>> GetList()
        {
            try
            {
                List<TipoIdentificacion> lista = new List<TipoIdentificacion>();
                lista = await _dbContext.TipoIdentificacions.ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
