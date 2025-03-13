using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesHomes.Const;
using SalesHomes.DTOs;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Services
{
    public class AgencyService
    {
        private readonly IAgencyManager _agency;
        public AgencyService(IAgencyManager agency)
        {
            _agency = agency;
        }

        public Agency GetAgencyById(int id) {
            _ = id <= 0 ? throw new ArgumentNullException(nameof(id), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            return _agency.GetAgencyById(id);
        }

        public async Task<AgencyResponse> CreateAgency(Agency agency) {
            _ = agency ?? throw new ArgumentNullException(nameof(agency));
            _ = string.IsNullOrEmpty(agency.Address) ? throw new ArgumentNullException(nameof(agency), ValidationMessages.REQUIRED_REGISTRATION) : string.Empty;
            _ = string.IsNullOrEmpty(agency.Phone) ? throw new ArgumentNullException(nameof(agency), ValidationMessages.REQUIRED_REGISTRATION) : string.Empty;
            _ = string.IsNullOrEmpty(agency.Name) ? throw new ArgumentNullException(nameof(agency), ValidationMessages.REQUIRED_REGISTRATION) : string.Empty;

            return await _agency.CreateAgency(agency);

        }

        public List<Agency> GetAllAgencys() { 
            return _agency.GetAllAgencys();
        }

    }
}