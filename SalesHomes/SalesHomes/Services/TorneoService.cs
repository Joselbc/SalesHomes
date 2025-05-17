using SalesHomes.Models;
using SalesHomes.Services.Ports;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesHomes.Services
{
    public class TorneoService
    {
        private readonly ITorneoRepository _torneoRepository;

        public TorneoService(ITorneoRepository torneoRepository)
        {
            _torneoRepository = torneoRepository;
        }

        public async Task<bool> AgregarTorneo(Torneos torneos)
        {
            try
            {
                ValidarValoresTorneo(torneos);
                return await _torneoRepository.AgregarTorneo(torneos);
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Error de validación: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al agregar el torneo: {ex.Message}");
            }
        }

        public async Task<List<Torneos>> ObtenerTorneos()
        {
            try
            {
                return await _torneoRepository.ObtenerTorneos();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al obtener los torneos: {ex.Message}");
            }
        }
        public async Task<Torneos> ObtenerTorneoPorId(int id)
        {
            try
            {
                var torneo = await _torneoRepository.ObtenerTorneoPorId(id);
                if (torneo == null)
                {
                    throw new Exception("El torneo no fue encontrado.");
                }
                return torneo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al obtener el torneo por ID: {ex.Message}");
            }
        }
        public async Task<object> ObtenerTorneoPorTipo(string tipo)
        {
            try
            {
                if (string.IsNullOrEmpty(tipo))
                {
                    throw new ArgumentNullException("El tipo de torneo no puede ser nulo o vacío.");
                }

                var torneo = await _torneoRepository.ObtenerTorneoPorTipo(tipo);
                if (torneo == null)
                {
                    return new { mensaje = "No se encontro el torneo" };
                }

                return torneo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al obtener el torneo por tipo: {ex.Message}");
            }
        }
        public async Task<object> ObtenerTorneoPorNombre(string nombre)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre))
                {
                    throw new ArgumentNullException("El nombre del torneo no puede ser nulo o vacío.");
                }

                var torneo = await _torneoRepository.ObtenerTorneoPorTipo(nombre);
                if (torneo == null)
                {
                    return new { mensaje = "No se encntro el torneo" };
                }

                return torneo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al obtener el torneo por nombre: {ex.Message}");
            }
        }

        public async Task<object> ObtenerTorneoPorFecha(DateTime fecha)
        {
            try
            {
                var torneo = await _torneoRepository.ObtenerTorneoPorFecha(fecha);
                if (torneo == null)
                {
                    return new { mensaje = "No se encontro el torneo" };
                }

                return torneo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al obtener el torneo por fecha: {ex.Message}");
            }
        }

        public async Task<bool> ActualizarTorneo(Torneos torneo)
        {
            try
            {
                ValidarValoresTorneo(torneo);
                return await _torneoRepository.ActualizarTorneo(torneo);
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Error de validación: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al actualizar el torneo: {ex.Message}");
            }
        }
        public async Task<bool> EliminarTorneo(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("El ID del torneo no es válido.");
                }
                return await _torneoRepository.EliminarTorneo(id);
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Error de validación: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al eliminar el torneo: {ex.Message}");
            }
        }
        private void ValidarValoresTorneo(Torneos torneos)
        {

            if (torneos.FechaTorneo == null)
            {
                throw new ArgumentNullException("La fecha del torneo es obligatoria.");
            }
            if (string.IsNullOrEmpty(torneos.NombreTorneo))
            {
                throw new ArgumentNullException("El nombre del torneo es obligatorio.");
            }
            if (string.IsNullOrEmpty(torneos.TipoTorneo))
            {
                throw new ArgumentNullException("El tipo de torneo es obligatorio.");
            }
            if (torneos.idAdministradorITM < 0)
            {
                throw new ArgumentNullException("El ID del administrador es inválido.");
            }
            if (torneos.ValorInscripcion <= 0)
            {
                throw new ArgumentNullException("El valor de inscripción debe ser mayor a cero.");
            }
        }
    }
}

