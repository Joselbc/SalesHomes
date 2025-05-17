using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SalesHomes.Models;
using SalesHomes.Services;

namespace SalesHomes.Controllers
{
    [RoutePrefix("api/admin")]
    [Authorize]
    public class AdministradorITMController : ApiController
    {
        private readonly AdministradorITMService _administradorITM;

        public AdministradorITMController(AdministradorITMService administradorITM)
        {
            _administradorITM = administradorITM;
        }

        [HttpPost]
        [Route("agregar")]
        public async Task<IHttpActionResult> AgregarAdministrador(AdministradorITM nuevoAdministrador)
        {
            return Ok(await _administradorITM.AgregarAdministrador(nuevoAdministrador));
        }

        [HttpGet]
        [Route("todos")]
        public async Task<IHttpActionResult> ObtenerTodosLosAdministradores()
        {
            return Ok(await _administradorITM.ObtenerTodosLosAdministradores());
        }
    }
}