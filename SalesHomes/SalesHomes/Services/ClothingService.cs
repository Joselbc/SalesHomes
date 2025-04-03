using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesHomes.DTOs;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Services
{
    public class ClothingService
    {
        private readonly IClothingRepository _clothingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IImageRepository _imageRepository;

        public ClothingService(IClothingRepository clothingRepository, ICustomerRepository customerRepository, IImageRepository imageRepository)
        {
            _clothingRepository = clothingRepository;
            _customerRepository = customerRepository;
            _imageRepository = imageRepository;
        }

        public async Task<bool> AddClothingAsync(Prenda clothingItem)
        {
            Cliente cliente =  _customerRepository.GetCustomer(clothingItem.Cliente);
            _ = cliente == null ? throw new ArgumentNullException(nameof(cliente), $"El cliente don dicho documento no existe {clothingItem.Cliente}") : cliente;
            return await _clothingRepository.AddClothingAsync(clothingItem);
        }

        public List<ClothingByCustomerDto> GetClothingByCustomer(string document)
        {
            List<ClothingByCustomerDto> clothingByCustomerDtos = new List<ClothingByCustomerDto>();
            Cliente customer = _customerRepository.GetCustomer(document);
            _ = customer == null ? throw new ArgumentNullException(nameof(customer), $"El cliente don dicho documento no existe {document}") : customer;
            List<Prenda> clothing = _clothingRepository.GetClothingByCustomer(document);

            foreach (var item in clothing)
            {
                List<FotoPrenda> photoClothing =  _imageRepository.GetImageListById(item.IdPrenda);
                clothingByCustomerDtos.Add(new ClothingByCustomerDto { 
                    Clothing = item,
                    PhotoClothing = photoClothing
                });
            }

            return clothingByCustomerDtos;
        }
    }
}
