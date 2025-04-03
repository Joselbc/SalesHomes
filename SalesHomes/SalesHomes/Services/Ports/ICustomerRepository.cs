using SalesHomes.Models;
using System.Threading.Tasks;

namespace SalesHomes.Services.Ports
{
    public interface ICustomerRepository
    {
        Task<bool> AddCustomerAsync(Cliente customer);
        Cliente GetCustomer(string document);
    }
}
