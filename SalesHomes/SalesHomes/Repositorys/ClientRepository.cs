using System.Linq;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Repositorys
{
    public class ClientRepository : IClientManager
    {
        private readonly ITMHousingSalesEntities _repository;

        public ClientRepository(ITMHousingSalesEntities repository)
        {
            _repository = repository;
        }

        public Client GetClientById(int id)
        {
            return _repository.Client.FirstOrDefault(a => a.ClientId == id);
        }

    }
}