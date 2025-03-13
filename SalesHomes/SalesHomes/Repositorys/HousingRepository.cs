using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using SalesHomes.Const;
using SalesHomes.DTOs;
using SalesHomes.Enums;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Repositorys
{
    public class HousingRepository : IHousingManager
    {
        private readonly ITMHousingSalesEntities _repository;

        public HousingRepository(ITMHousingSalesEntities repository)
        {
            _repository = repository;
        }

        public Housing GetHousingById(int id)
        {
            return _repository.Housing.FirstOrDefault(a => a.HousingId == id);
        }
        public Housing GetAvailableHousingById(int id)
        {
            string statusName = Enum.GetName(typeof(HousingStatusEnum), HousingStatusEnum.Available);
            return _repository.Housing.FirstOrDefault(a => a.HousingId == id && a.Status.Equals(statusName));
        }
        public Housing UpdateHousing(Housing housing) {
            var existingHousing = _repository.Housing.Find(housing.HousingId);
            if (existingHousing == null)
            {
                throw new KeyNotFoundException(ValidationMessages.HOUSING_NOT_AVAILABLE);
            }

            _repository.Entry(existingHousing).CurrentValues.SetValues(housing);
            _repository.SaveChanges();

            return existingHousing;
        }
        public List<HousingType> GetHousingsTypes()
        {
            return _repository.HousingType.ToList();
        }

        public HousingType GetHousingsTypes(int id)
        {
            return _repository.HousingType.FirstOrDefault(h => h.HousingTypeId == id);
        }

        public List<Housing> GetHousesByStatus(string status)
        {
            return _repository.Housing.Where(a => a.Status.Equals(status)).ToList();
        }

        public async Task<HousingResponse> CreateHousing(Housing housing)
        {
            try
            {
                _repository.Housing.Add(housing);
                await _repository.SaveChangesAsync();

                return new HousingResponse
                {
                    HousingId = housing.HousingId.ToString(),
                    Message = ValidationMessages.CREATED_HOUSING,
                    Registered = true
                };
            }
            catch (Exception e)
            {
                return new HousingResponse
                {
                    HousingId = housing.HousingId.ToString(),
                    Message = ValidationMessages.HOUSING_CREATION_ERROR,
                    Registered = false
                };
            }
        }

    }
}