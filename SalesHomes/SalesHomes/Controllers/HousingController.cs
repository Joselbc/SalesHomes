

using SalesHomes.DTOs;
using SalesHomes.Enums;
using SalesHomes.Models;
using SalesHomes.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace SalesHomes.Controllers
{
    [RoutePrefix("api/housing")]
    public class HousingController : ApiController
    {
        private readonly HousingService _HousingService;
        public HousingController(HousingService HousingService)
        {
            _HousingService = HousingService;
        }

        [HttpGet]
        [Route("get-housings-types")]
        public IHttpActionResult GetHousingsTypes()
        {
            return Ok(_HousingService.GetHousingsTypes());
        }

        [HttpGet]
        [Route("get-sold-houses")]
        public IHttpActionResult GetSoldHouses()
        {
            return Ok(_HousingService.GetHousesByStatus(HousingStatusEnum.Sold.ToString()));
        }

        [HttpGet]
        [Route("get-available-houses")]
        public IHttpActionResult GetAvailableHouses()
        {
            return Ok(_HousingService.GetHousesByStatus(HousingStatusEnum.Available.ToString()));
        }

        [HttpGet]
        [Route("get-reserved-houses")]
        public IHttpActionResult GetReservedHouses()
        {
            return Ok(_HousingService.GetHousesByStatus(HousingStatusEnum.Reserved.ToString()));
        }

        [HttpPatch]
        [Route("reserved-houses")]
        public IHttpActionResult ReservedHouses(string id)
        {
            return Ok(_HousingService.ReservedHouses(id));
        }

        [HttpPut]
        [Route("update-status-houses")]
        public IHttpActionResult UpdateStatusHouses([FromBody] UpdateHousingStatus housingStatus)
        {
            return Ok(_HousingService.UpdateStatusHouses(housingStatus));
        }

        [HttpPost]
        [Route("create-house")]
        public async Task<IHttpActionResult> CreateHouse([FromBody] Housing housing)
        {
            return Ok(await _HousingService.CreateHousing(housing));
        }
    }
}