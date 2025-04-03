using System.Data.Entity.Validation;
using System;
using System.Linq;
using System.Threading.Tasks;
using SalesHomes.Models;
using SalesHomes.Services.Ports;
using System.Collections.Generic;
using System.Data.Entity;

namespace SalesHomes.Repositorys
{
    public class ImageRepository : IImageRepository
    {
        private readonly DBExamenEntities _db;

        public ImageRepository(DBExamenEntities db)
        {
            _db = db;
        }

        public async Task<bool> AddImageAsync(FotoPrenda image)
        {
            try
            {
                int newId = (_db.FotoPrenda.OrderByDescending(p => p.idFoto).Select(p => p.idFoto).FirstOrDefault()) + 1;
                image.idFoto = newId;

                _db.FotoPrenda.Add(image);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Error en la propiedad {validationError.PropertyName}: {validationError.ErrorMessage}");
                    }
                }
                throw;
            }
        }

        public async Task<FotoPrenda> GetImageByIdAsync(int id)
        {
            return await _db.FotoPrenda.FindAsync(id);
        }

        public List<FotoPrenda> GetImageListById(int id)
        {
            return _db.FotoPrenda.Where(fp => fp.idPrenda == id).ToList();
        }

        public async Task<bool> UpdateImageAsync(FotoPrenda image)
        {
            var existingImage = await _db.FotoPrenda.FindAsync(image.idFoto);
            if (existingImage == null)
                return false;

            existingImage.FotoPrenda1 = image.FotoPrenda1;
            existingImage.idPrenda = image.idPrenda;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteImageAsync(int id)
        {
            var image = await _db.FotoPrenda.FindAsync(id);
            if (image == null)
                return false;

            _db.FotoPrenda.Remove(image);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}

