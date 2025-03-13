using AutoMapper;
using SalesHomes.DTOs;
using SalesHomes.Models;

namespace SalesHomes.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleResponse, Sale>()
               .ForPath(dest => dest.Agency.Name, opt => opt.MapFrom(src => src.Agency))
               .ForPath(dest => dest.Client.FirstName, opt => opt.MapFrom(src => src.NameClient))
               .ForPath(dest => dest.Client.IdentityDocument, opt => opt.MapFrom(src => src.Document))
               .ForPath(dest => dest.Housing.HousingType.Description, opt => opt.MapFrom(src => src.TypeHousing))
               .ForPath(dest => dest.Housing.Price, opt => opt.MapFrom(src => src.Price))
               .ForPath(dest => dest.Housing.SizeM2, opt => opt.MapFrom(src => src.SizeM2))
               .ReverseMap();
        }
    }
}

