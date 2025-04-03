using System.Linq;
using System.Threading.Tasks;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Repositorys
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DBExamenEntities _db;

        public CustomerRepository(DBExamenEntities db)
        {
            _db = db;
        }

        public async Task<bool> AddCustomerAsync(Cliente customer)
        {

            _db.Cliente.Add(customer);
            await _db.SaveChangesAsync();
            return true;
        }

        public Cliente GetCustomer(string document) {
            return _db.Cliente.FirstOrDefault(client => client.Documento.Equals(document, System.StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
