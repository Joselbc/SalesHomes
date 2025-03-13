using SalesHomes.DTOs;
using SalesHomes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesHomes.Services.Ports
{
    public interface IAgencyManager
    {
        Agency GetAgencyById(int id);
        Task<AgencyResponse> CreateAgency(Agency agency);
        List<Agency> GetAllAgencys();
    }
}
