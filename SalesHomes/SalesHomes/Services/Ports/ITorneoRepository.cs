using SalesHomes.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesHomes.Services.Ports
{
    public   interface ITorneoRepository
    {
        Task<bool> AgregarTorneo(Torneos torneos);
        Task<List<Torneos>> ObtenerTorneos();
        Task<Torneos> ObtenerTorneoPorId(int id);
        Task<Torneos> ObtenerTorneoPorTipo(string tipo);
        Task<Torneos> ObtenerTorneoPorNombre(string nombre);
        Task<Torneos> ObtenerTorneoPorFecha(DateTime fecha);
        Task<bool> ActualizarTorneo(Torneos torneo);
        Task<bool> EliminarTorneo(int id);
    }
}
