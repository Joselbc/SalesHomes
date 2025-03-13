using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesHomes.Const;
using SalesHomes.DTOs;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Repositorys
{
    public class AgencyRepository : IAgencyManager
    {
        private readonly ITMHousingSalesEntities _repository;

        public AgencyRepository(ITMHousingSalesEntities repository)
        {
            _repository = repository;
        }

        public Agency GetAgencyById(int id)
        {
            return _repository.Agency.FirstOrDefault(a => a.AgencyId == id);
        }

        public async Task<AgencyResponse> CreateAgency(Agency agency)
        {

            try
            {
                _repository.Agency.Add(agency);
                await _repository.SaveChangesAsync();

                return new AgencyResponse
                {
                    AgencyId = agency.AgencyId.ToString(),
                    Message = ValidationMessages.CREATED_AGENCY,
                    Registered = true
                };
            }
            catch (Exception e)
            {
                return new AgencyResponse
                {
                    AgencyId = agency.AgencyId.ToString(),
                    Message = ValidationMessages.AGENCY_CREATION_ERROR,
                    Registered = true
                };
            }
        }

        public List<Agency> GetAllAgencys()
        {
            return _repository.Agency.ToList();
        }
    }

}