using System.Collections.Generic;
using System.Data.Entity;
using SalesHomes.DTOs;
using SalesHomes.Models;

namespace SalesHomes.Services.Ports
{
    public interface ISaleManager
    {
        List<SaleResponse> GetAllSales();
        RegisterSaleResponse RegisterSale(Sale sale, Housing housing);
        RegisterSaleResponse DeleteSale(int saleId);

    }
}