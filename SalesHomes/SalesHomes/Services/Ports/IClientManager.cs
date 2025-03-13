using SalesHomes.Models;

namespace SalesHomes.Services.Ports
{
    public interface IClientManager
    {
        Client GetClientById(int id);
    }
}
