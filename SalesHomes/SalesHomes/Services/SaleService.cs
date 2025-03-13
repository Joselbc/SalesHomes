using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SalesHomes.Const;
using SalesHomes.DTOs;
using SalesHomes.Enums;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Services
{
    public class SaleService
    {
        private readonly ISaleManager _sale;
        private readonly HousingService _houseService;
        private readonly AgencyService _agencyService;
        private readonly ClientService _clientService;
        public SaleService(ISaleManager sale, ClientService clientService,
                           AgencyService agencyService, HousingService housingService)
        {
            _sale = sale;
            _clientService = clientService;
            _houseService = housingService;
            _agencyService = agencyService;
        }

        public List<SaleResponse> GetAllSales() {
            return _sale.GetAllSales();
        }

        public RegisterSaleResponse RegisterSale(Sale sale) {
            _ = sale ?? throw new ArgumentNullException(nameof(sale));
            _ = sale.ClientId <= 0 ? throw new ArgumentNullException(nameof(sale), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            _ = sale.HousingId <= 0 ? throw new ArgumentNullException(nameof(sale), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            _ = sale.AgencyId <= 0 ? throw new ArgumentNullException(nameof(sale), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            _ = sale.SalePrice <= 0 ? throw new ArgumentNullException(nameof(sale), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            var noExists = new List<string>();

            Client client = _clientService.GetClientById(sale.ClientId);
            Housing housing = _houseService.GetAvailableHousingById(sale.HousingId);
            Agency agency = _agencyService.GetAgencyById(sale.AgencyId);

            if (client == null) noExists.Add(ValidationMessages.CLIENT_NOT_FOUND);
            if (housing == null) noExists.Add(ValidationMessages.HOUSING_NOT_AVAILABLE);
            if (agency == null) noExists.Add(ValidationMessages.AGENCY_NOT_FOUND);

            if (noExists.Any())
            {
                return new RegisterSaleResponse
                {
                    SaleId = sale.SaleId.ToString(),
                    Message = string.Join(" ", noExists),
                    Registered = false
                };
            }

            sale.SaleDate = DateTime.Now;
            housing.Status = Enum.GetName(typeof(HousingStatusEnum), HousingStatusEnum.Sold);

            return _sale.RegisterSale(sale, housing);
        }

        public RegisterSaleResponse DeleteSale(int saleId)
        {
            _ = saleId <= 0 ? throw new ArgumentNullException(nameof(saleId), ValidationMessages.REQUIRED_REGISTRATION) : default(int);
            return _sale.DeleteSale(saleId);
        }


    }
}