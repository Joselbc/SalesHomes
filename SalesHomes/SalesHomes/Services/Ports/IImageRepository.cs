using SalesHomes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesHomes.Services.Ports
{
    public   interface IImageRepository
    {
        Task<bool> AddImageAsync(FotoPrenda image);
        Task<bool> DeleteImageAsync(int id);
        Task<bool> UpdateImageAsync(FotoPrenda image);
        Task<FotoPrenda> GetImageByIdAsync(int id);
        List<FotoPrenda> GetImageListById(int id);

    }
}
