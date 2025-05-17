using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using SalesHomes.Models;
using SalesHomes.Services.Ports;
using System.Data.Entity;

namespace SalesHomes.Repositorys
{
    public class AdministradorITMRepository : IAdministradorITMRepository
    {
        private readonly DBExamenEntities1 _db;

        public AdministradorITMRepository(DBExamenEntities1 db)
        {
            _db = db;
        }

        public async Task<AdministradorITM> AgregarAdministrador(AdministradorITM nuevoAdministrador)
        {
            if (nuevoAdministrador == null)
                throw new ArgumentNullException(nameof(nuevoAdministrador));

            _db.AdministradorITM.Add(nuevoAdministrador);
           await  _db.SaveChangesAsync(); 
            return nuevoAdministrador; 
        }

        public async Task<List<AdministradorITM>> ObtenerTodosLosAdministradores()
        {
            return await _db.AdministradorITM.ToListAsync(); 
        }
    }
}
