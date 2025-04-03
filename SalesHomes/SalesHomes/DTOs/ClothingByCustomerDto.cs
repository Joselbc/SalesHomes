using System.Collections.Generic;
using SalesHomes.Models;

namespace SalesHomes.DTOs
{
    public class ClothingByCustomerDto
    {
        public Prenda Clothing { get; set; }
        public List<FotoPrenda> PhotoClothing { get; set; }

    }
}
