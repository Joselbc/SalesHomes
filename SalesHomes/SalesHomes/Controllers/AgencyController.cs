


using SalesHomes.Models;
using SalesHomes.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace SalesHomes.Controllers
{
    [RoutePrefix("api/agency")]
    public class AgencyController : ApiController
    {
        private readonly AgencyService _AgencyService;
        public AgencyController(AgencyService AgencyService)
        {
            _AgencyService = AgencyService;
        }

        [HttpPost]
        [Route("create-agency")]
        public async Task<IHttpActionResult> CreateAgency([FromBody] Agency agency)
        {
            return Ok(await _AgencyService.CreateAgency(agency));
        }

        [HttpGet]
        [Route("get-all-agencys")]
        public IHttpActionResult GetAllAgencys() {
            return Ok(_AgencyService.GetAllAgencys());
        }

        [HttpGet]
        [Route("get-agency-by-id")]
        public IHttpActionResult GetAgencyById(string id)
        {
            int idAgency = int.Parse(id);
            return Ok(_AgencyService.GetAgencyById(idAgency));
        }
    }
}