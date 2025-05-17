using SalesHomes.Entitys;
using SalesHomes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SalesHomes.Controllers
{
    [RoutePrefix("api/auth")]
    [AllowAnonymous]
    public class AuthController : ApiController
    {
        private readonly JwtAuthManagerService _authManager;

        public AuthController()
        {
            _authManager = new JwtAuthManagerService("estaSeriaLaClaveSecreta");
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] AccesoUsuario loginModel)
        {
            if (loginModel == null)
                return BadRequest("Invalid data.");

            if (_authManager.ValidacionUsuario(loginModel))
            {
                var token = _authManager.GenerateToken(loginModel.Usuario);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }

}