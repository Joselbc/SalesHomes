using System.Threading.Tasks;
using System.Web.Http;
using SalesHomes.Models;
using SalesHomes.Services;

namespace SalesHomes.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Route("add-customer")]
        public async Task<IHttpActionResult> AddCustomer([FromBody] Cliente customer)
        {
            return Ok(await _customerService.AddCustomerAsync(customer));
        }
    }
}
