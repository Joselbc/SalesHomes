using System.Collections.Generic;
using System.Threading.Tasks;
using SalesHomes.Models;

namespace SalesHomes.Services.Ports
{
    public interface IClothingRepository
    {
        List<Prenda> GetClothingByCustomer(string customerId);
        Task<bool> AddClothingAsync(Prenda clothingItem);
    }
}
