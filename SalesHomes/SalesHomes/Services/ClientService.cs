using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SalesHomes.Const;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Services
{
    public class ClientService
    {
        private readonly IClientManager _client;
        public ClientService(IClientManager client)
        {
            _client = client;
        }

        public Client GetClientById(int id)
        {
            _ = id <= 0 ? throw new ArgumentNullException(nameof(id), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            return _client.GetClientById(id);
        }
    }
}