using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesHomes.Const;
using SalesHomes.DTOs;
using SalesHomes.Enums;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Services
{
    public class HousingService
    {
        private readonly IHousingManager _housing;
        public HousingService(IHousingManager housing)
        {
            _housing = housing;
        }

        public Housing GetHousingById(int id)
        {
            _ = id <= 0 ? throw new ArgumentNullException(nameof(id), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            return _housing.GetHousingById(id);
        }
        public Housing GetAvailableHousingById(int id)
        {
            _ = id <= 0 ? throw new ArgumentNullException(nameof(id), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            return _housing.GetAvailableHousingById(id);
        }

        public HousingType GetHousingsTypes(int id) {
            return _housing.GetHousingsTypes(id);
        }
        public Housing UpdateHousing(Housing housing) {
            return _housing.UpdateHousing(housing);
        }

        public List<HousingType> GetHousingsTypes() {
            return _housing.GetHousingsTypes();
        }

        public HousingResponse ReservedHouses(string id) {
            try
            {
                _ = string.IsNullOrEmpty(id) ? throw new ArgumentNullException(nameof(id), ValidationMessages.REQUIRED_REGISTRATION) : string.Empty;
                int HousingId = int.Parse(id);
                _ = HousingId <= 0 ? throw new ArgumentNullException(nameof(HousingId), ValidationMessages.REQUIRED_REGISTRATION) : default(int);

                Housing housing = _housing.GetHousingById(HousingId);
                if (housing == null)
                {
                    return new HousingResponse
                    {
                        HousingId = housing.HousingId.ToString(),
                        Message = ValidationMessages.HOUSING_NOT_AVAILABLE,
                        Registered = false
                    };
                }
                housing.Status = Enum.GetName(typeof(HousingStatusEnum), HousingStatusEnum.Reserved);

                housing = _housing.UpdateHousing(housing);
                return new HousingResponse
                {
                    HousingId = housing.HousingId.ToString(),
                    Message = ValidationMessages.RESERVATION_SUCCESSFUL,
                    Registered = true
                };
            }
            catch (Exception e)
            {
                return new HousingResponse
                {
                    HousingId = id.ToString(),
                    Message = ValidationMessages.HOUSING_NOT_AVAILABLE,
                    Registered = false
                };
            }

        }

        public object UpdateStatusHouses(UpdateHousingStatus housingStatus)
        {
            try
            {
                _ = housingStatus ?? throw new ArgumentNullException(nameof(housingStatus));
                _ = string.IsNullOrEmpty(housingStatus.Status) ? throw new ArgumentNullException(nameof(housingStatus.Status), ValidationMessages.REQUIRED_REGISTRATION) : string.Empty;
                _ = string.IsNullOrEmpty(housingStatus.Id) ? throw new ArgumentNullException(nameof(housingStatus.Id), ValidationMessages.REQUIRED_REGISTRATION) : string.Empty;
                int HousingId = int.Parse(housingStatus.Id);
                _ = HousingId <= 0 ? throw new ArgumentNullException(nameof(HousingId), ValidationMessages.REQUIRED_REGISTRATION) : default(int);

                if (!ValidateStatus(housingStatus.Status)) {
                    return new
                    {
                        Message = ValidationMessages.INVALID_HOUSING_STATUS,
                        ValidStatuses = GetValidStatements()
                    };
                }

                Housing housing = _housing.GetHousingById(HousingId);
                if (housing == null)
                {
                    return new HousingResponse
                    {
                        HousingId = housing.HousingId.ToString(),
                        Message = ValidationMessages.HOUSING_NOT_AVAILABLE,
                        Registered = false
                    };
                }
                housing.Status = housingStatus.Status;

                housing = _housing.UpdateHousing(housing);
                return new HousingResponse
                {
                    HousingId = housing.HousingId.ToString(),
                    Message = ValidationMessages.SUCCESSFUL_UPGRADE,
                    Registered = true
                };
            }
            catch (Exception e)
            {
                return new HousingResponse
                {
                    HousingId = housingStatus.Id,
                    Message = ValidationMessages.HOUSING_NOT_AVAILABLE,
                    Registered = false
                };
            }

        }

        public object GetHousesByStatus(string status)
        {
            _ = string.IsNullOrEmpty(status) ? throw new ArgumentNullException(nameof(status), ValidationMessages.REQUIRED_REGISTRATION) : string.Empty;

            if (!ValidateStatus(status))
            {
                return new
                {
                    Message = ValidationMessages.INVALID_HOUSING_STATUS,
                    ValidStatuses = GetValidStatements()
                };
            }
            return _housing.GetHousesByStatus(status);
        }

        public async Task<HousingResponse> CreateHousing(Housing housing)
        {
            _ = housing ?? throw new ArgumentNullException(nameof(housing));
            _ = housing.HousingTypeId <= 0 ? throw new ArgumentNullException(nameof(housing), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            _ = housing.RoomCount <= 0 ? throw new ArgumentNullException(nameof(housing), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            _ = housing.BathroomCount <= 0 ? throw new ArgumentNullException(nameof(housing), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            _ = housing.SizeM2 <= 0 ? throw new ArgumentNullException(nameof(housing), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            _ = housing.FloorCount <= 0 ? throw new ArgumentNullException(nameof(housing), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            _ = housing.Price <= 0 ? throw new ArgumentNullException(nameof(housing), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            _ = string.IsNullOrEmpty(housing.Accessories) ? throw new ArgumentNullException(nameof(housing), ValidationMessages.REQUIRED_REGISTRATION) : string.Empty;

            housing.Status = Enum.GetName(typeof(HousingStatusEnum), HousingStatusEnum.Available);

            HousingType housingType = _housing.GetHousingsTypes(housing.HousingTypeId);

            if (housingType == null) {
                return new HousingResponse
                {
                    HousingId = housing.HousingId.ToString(),
                    Message = ValidationMessages.HOUSING_NOT_AVAILABLE,
                    Registered = false
                };
            }

            return await _housing.CreateHousing(housing);
        }

        private bool ValidateStatus(string status) {

            string availableStatus = Enum.GetName(typeof(HousingStatusEnum), HousingStatusEnum.Available);
            string soldStatus = Enum.GetName(typeof(HousingStatusEnum), HousingStatusEnum.Sold);
            string reservedStatus = Enum.GetName(typeof(HousingStatusEnum), HousingStatusEnum.Reserved);

            if (!string.Equals(status, availableStatus, StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(status, soldStatus, StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(status, reservedStatus, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private object GetValidStatements() {
            return new[] 
            { 
                Enum.GetName(typeof(HousingStatusEnum), HousingStatusEnum.Available),
                Enum.GetName(typeof(HousingStatusEnum), HousingStatusEnum.Sold),
                Enum.GetName(typeof(HousingStatusEnum), HousingStatusEnum.Reserved)
            };
        }
    }
}