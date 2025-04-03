using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Services
{
    public class ImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly string _imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "Images");

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
            try
            {
                if (!Directory.Exists(_imagePath))
                {
                    Directory.CreateDirectory(_imagePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear la carpeta de imágenes: {ex.Message}");
            }
        }

        public async Task<bool> AddImageAsync(byte[] imageData, int clothingId)
        {
            if (imageData == null || imageData.Length == 0)
                throw new ArgumentException("Archivo inválido.");

            string fileName = Guid.NewGuid() + ".png";
            string fullPath = Path.Combine(_imagePath, fileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
            {
                await fileStream.WriteAsync(imageData, 0, imageData.Length);
            }

            var image = new FotoPrenda
            {
                FotoPrenda1 = fileName,
                idPrenda = clothingId
            };
            return await _imageRepository.AddImageAsync(image);
        }


        public async Task<bool> UpdateImageAsync(int imageId, byte[] imageData)
        {
            var existingImage = await _imageRepository.GetImageByIdAsync(imageId);
            if (existingImage == null)
                throw new KeyNotFoundException("Imagen no encontrada.");

            string oldFilePath = Path.Combine(_imagePath, existingImage.FotoPrenda1);

            if (File.Exists(oldFilePath))
                File.Delete(oldFilePath);

            string fileName = Guid.NewGuid() + ".png";
            string newFilePath = Path.Combine(_imagePath, fileName);

            using (var fileStream = new FileStream(newFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
            {
                await fileStream.WriteAsync(imageData, 0, imageData.Length);
            }

            existingImage.FotoPrenda1 = fileName;
            return await _imageRepository.UpdateImageAsync(existingImage);
        }


        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var existingImage = await _imageRepository.GetImageByIdAsync(imageId);
            if (existingImage == null)
                throw new KeyNotFoundException("Imagen no encontrada.");

            string filePath = Path.Combine(_imagePath, existingImage.FotoPrenda1);

            if (!File.Exists(filePath))
                return false;

            File.Delete(filePath);
            return await _imageRepository.DeleteImageAsync(imageId);
        }

        public async Task<string> GetImageByIdAsync(int imageId)
        {
            var image = await _imageRepository.GetImageByIdAsync(imageId);
            if (image == null)
                return null;

            string filePath = Path.Combine(_imagePath, image.FotoPrenda1);
            if (!File.Exists(filePath))
                return null;

            try
            {
                byte[] imageBytes = await Task.Run(() => File.ReadAllBytes(filePath));

                if (imageBytes == null || imageBytes.Length == 0)
                    return null;

                return Convert.ToBase64String(imageBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer la imagen: {ex.Message}");
                return null;
            }
        }
    }
}
