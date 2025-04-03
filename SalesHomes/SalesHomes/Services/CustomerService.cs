using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> AddCustomerAsync(Cliente customer)
        {
            return await _customerRepository.AddCustomerAsync(customer);
        }
    }
}
