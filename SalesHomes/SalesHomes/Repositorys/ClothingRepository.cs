using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Repositorys
{
    public class ClothingRepository : IClothingRepository
    {
        private readonly DBExamenEntities _db;

        public ClothingRepository(DBExamenEntities db)
        {
            _db = db;
        }

        public async Task<bool> AddClothingAsync(Prenda clothingItem)
        {
            int newId = (_db.Prenda.OrderByDescending(p => p.IdPrenda).Select(p => p.IdPrenda).FirstOrDefault()) + 1;

            clothingItem.IdPrenda = newId;
            _db.Prenda.Add(clothingItem);
            await _db.SaveChangesAsync();
            return true;
        }

        public List<Prenda> GetClothingByCustomer(string customerId)
        {
            return _db.Prenda.Where(p => p.Cliente == customerId).ToList();
        }
    }
}
