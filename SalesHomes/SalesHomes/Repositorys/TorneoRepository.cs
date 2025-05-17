using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Repositorys
{
    public class TorneoRepository : ITorneoRepository
    {
        private readonly DBExamenEntities1 _db;

        public TorneoRepository(DBExamenEntities1 db)
        {
            _db = db;
        }

        public async Task<bool> AgregarTorneo(Torneos torneos)
        {
            int newId = (_db.Torneos.OrderByDescending(p => p.idTorneos).Select(p => p.idTorneos).FirstOrDefault()) + 1;
            torneos.idTorneos = newId;
            _db.Torneos.Add(torneos);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Torneos>> ObtenerTorneos()
        {
            return await _db.Torneos.ToListAsync();
        }

        public async Task<Torneos> ObtenerTorneoPorId(int id)
        {
            return await _db.Torneos.FirstOrDefaultAsync(t => t.idTorneos == id);
        }

        public async Task<Torneos> ObtenerTorneoPorTipo(string tipo)
        {
            return await _db.Torneos.FirstOrDefaultAsync(t => t.TipoTorneo == tipo);
        }

        public async Task<Torneos> ObtenerTorneoPorNombre(string nombre)
        {
            return await _db.Torneos.FirstOrDefaultAsync(t => t.NombreTorneo == nombre);
        }
        public async Task<Torneos> ObtenerTorneoPorFecha(DateTime fecha)
        {
            return await _db.Torneos.FirstOrDefaultAsync(t => t.FechaTorneo == fecha);
        }
        public async Task<bool> ActualizarTorneo(Torneos torneo)
        {
            var torneoExistente = await _db.Torneos.FirstOrDefaultAsync(t => t.idTorneos == torneo.idTorneos);
            if (torneoExistente == null)
                return false;

            torneoExistente.NombreTorneo = torneo.NombreTorneo;
            torneoExistente.TipoTorneo = torneo.TipoTorneo;
            torneoExistente.NombreEquipo = torneo.NombreEquipo;
            torneoExistente.ValorInscripcion= torneo.ValorInscripcion;
            torneoExistente.FechaTorneo = torneo.FechaTorneo;
            torneoExistente.Integrantes = torneo.Integrantes;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarTorneo(int id)
        {
            var torneo = await _db.Torneos.FirstOrDefaultAsync(t => t.idTorneos == id);
            if (torneo == null)
                return false;

            _db.Torneos.Remove(torneo);
            await _db.SaveChangesAsync();
            return true;
        }

    }
}
