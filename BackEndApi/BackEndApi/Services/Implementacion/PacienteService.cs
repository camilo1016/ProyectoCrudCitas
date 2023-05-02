
using Microsoft.EntityFrameworkCore;
using BackEndApi.Models;
using BackEndApi.Services.Contrato;

namespace BackEndApi.Services.Implementacion
{
    public class PacienteService : IPacienteService
    {
        private DbpacienteContext _dbContext;

        public PacienteService(DbpacienteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Paciente>> GetList()
        {
            try
            {
                List<Paciente> lista = new List<Paciente>();
                lista = await _dbContext.Pacientes.Include(dpt => dpt.IdTipo).ToListAsync();

                return lista;

            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Paciente> Get(int idPaciente)
        {
            try
            {
                Paciente? encontrado = new Paciente();

                encontrado = await _dbContext.Pacientes.Include(dpt => dpt.IdTipo)
                    .Where(e => e.IdPaciente == idPaciente).FirstOrDefaultAsync();

                return encontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Paciente> Add(Paciente modelo)
        {
            try
            {
                _dbContext.Pacientes.Add(modelo);
                await _dbContext.SaveChangesAsync();

                return modelo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Paciente modelo)
        {
            try
            {
                _dbContext.Pacientes.Update(modelo);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(Paciente modelo)
        {
            try
            {
                _dbContext.Pacientes.Remove(modelo);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
