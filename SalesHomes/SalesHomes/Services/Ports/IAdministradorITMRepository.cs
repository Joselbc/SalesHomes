using SalesHomes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesHomes.Services.Ports
{
    public interface IAdministradorITMRepository
    {
        Task<List<AdministradorITM>> ObtenerTodosLosAdministradores();
        Task<AdministradorITM> AgregarAdministrador(AdministradorITM nuevoAdministrador);
    }
}
