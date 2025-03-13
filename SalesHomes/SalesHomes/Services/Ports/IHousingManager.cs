using SalesHomes.DTOs;
using SalesHomes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesHomes.Services.Ports
{
    public interface IHousingManager
    {
        Housing GetHousingById(int id);
        Housing GetAvailableHousingById(int id);
        Housing UpdateHousing(Housing housing);
        List<HousingType> GetHousingsTypes();
        HousingType GetHousingsTypes(int id);

        List<Housing> GetHousesByStatus(string status);
        Task<HousingResponse> CreateHousing(Housing housing);
    }
}
