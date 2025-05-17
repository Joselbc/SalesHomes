using System;
using System.Threading.Tasks;
using System.Web.Http;
using SalesHomes.Models;
using SalesHomes.Services;

namespace SalesHomes.Controllers
{
    [RoutePrefix("api/torneos")]
    [Authorize]
    public class TorneoController : ApiController
    {
        private readonly TorneoService _torneoService;

        public TorneoController(TorneoService clothingService)
        {
            _torneoService = clothingService;
        }
        // api/torneos/tipo/agregar
        [HttpPost]
        [Route("agregar")]
        public async Task<IHttpActionResult> AgregarTorneo([FromBody] Torneos torneo)
        {
            return  Ok(await _torneoService.AgregarTorneo(torneo));
        }
        // api/torneos
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> ObtenerTorneos()
        {
            return Ok(await _torneoService.ObtenerTorneos());
        }

        // api/torneos/tipo/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> ObtenerTorneoPorId(int id)
        {
           return Ok(await _torneoService.ObtenerTorneoPorId(id));
        }

        // api/torneos/tipo/{tipo}
        [HttpGet]
        [Route("tipo/{tipo}")]
        public async Task<IHttpActionResult> ObtenerTorneoPorTipo(string tipo)
        {
            return Ok(await _torneoService.ObtenerTorneoPorTipo(tipo));
        }

        // api/torneos/nombre/{nombre}
        [HttpGet]
        [Route("nombre/{nombre}")]
        public async Task<IHttpActionResult> ObtenerTorneoPorNombre(string nombre)
        {
            return Ok( await _torneoService.ObtenerTorneoPorNombre(nombre));
        }

        // api/torneos/fecha/{fecha}
        [HttpGet]
        [Route("fecha/{fecha}")]
        public async Task<IHttpActionResult> ObtenerTorneoPorFecha(DateTime fecha)
        {
            return Ok(await _torneoService.ObtenerTorneoPorFecha(fecha));
        }

        // api/torneos/actualizar
        [HttpPut]
        [Route("actualizar")]
        public async Task<IHttpActionResult> ActualizarTorneo([FromBody] Torneos torneo)
        {
            return Ok(await _torneoService.ActualizarTorneo(torneo));
        }

        // api/torneos/eliminar/{id}
        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<IHttpActionResult> EliminarTorneo(int id)
        {
            return Ok( await _torneoService.EliminarTorneo(id));
        }
    }

}

