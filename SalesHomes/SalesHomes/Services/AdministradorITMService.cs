using SalesHomes.Models;
using SalesHomes.Services.Ports;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesHomes.Services
{
    public class AdministradorITMService
    {
        private readonly IAdministradorITMRepository _administradorITMRepository;

        public AdministradorITMService(IAdministradorITMRepository administradorITMRepository)
        {
            _administradorITMRepository = administradorITMRepository;
        }
        public async Task<AdministradorITM> AgregarAdministrador(AdministradorITM nuevoAdministrador)
        {
            if (nuevoAdministrador == null)
                throw new ArgumentNullException(nameof(nuevoAdministrador));

            return await _administradorITMRepository.AgregarAdministrador(nuevoAdministrador);
        }

        public async Task<List<AdministradorITM>> ObtenerTodosLosAdministradores()
        {
            return await _administradorITMRepository.ObtenerTodosLosAdministradores();
        }

    }
}
