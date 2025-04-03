using System.Threading.Tasks;
using System.Web.Http;
using SalesHomes.Models;
using SalesHomes.Services;

namespace SalesHomes.Controllers
{
    [RoutePrefix("api/clothing")]
    public class ClothingController : ApiController
    {
        private readonly ClothingService _clothingService;

        public ClothingController(ClothingService clothingService)
        {
            _clothingService = clothingService;
        }

        [HttpPost]
        [Route("add-clothing")]
        public async Task<IHttpActionResult> AddClothing([FromBody] Prenda clothingItem)
        {
            return Ok(await _clothingService.AddClothingAsync(clothingItem));
        }

        [HttpGet]
        [Route("get-customer-clothing/{customerId}")]
        public IHttpActionResult GetCustomerClothing(string customerId)
        {
            return Ok(_clothingService.GetClothingByCustomer(customerId));
        }
    }

}
