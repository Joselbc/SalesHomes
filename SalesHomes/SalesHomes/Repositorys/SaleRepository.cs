using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using SalesHomes.Const;
using SalesHomes.DTOs;
using SalesHomes.Enums;
using SalesHomes.Models;
using SalesHomes.Services.Ports;

namespace SalesHomes.Repositorys
{
    public class SaleRepository : ISaleManager
    {
        private readonly ITMHousingSalesEntities _repository;
        private readonly IMapper _mapper;
        public SaleRepository(ITMHousingSalesEntities repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<SaleResponse> GetAllSales()
        {
            List<Sale> sales = _repository.Sale.ToList();
            return _mapper.Map<List<SaleResponse>>(sales);   
        }

        public RegisterSaleResponse RegisterSale(Sale sale, Housing housing)
        {
            using (var transaction = _repository.Database.BeginTransaction())
            {
                try
                {
                    _repository.Sale.Add(sale);
                    _repository.SaveChanges();

                    _repository.Housing.AddOrUpdate(housing);
                    _repository.SaveChanges();

                    transaction.Commit();
                    return new RegisterSaleResponse
                    {
                        SaleId = sale.SaleId.ToString(),
                        Message = ValidationMessages.CREATED_SALE,
                        Registered = true
                    };
                }
                catch (Exception e)
                {
                    transaction.Rollback(); 
                    return new RegisterSaleResponse
                    {
                        SaleId = sale.SaleId.ToString(),
                        Message = ValidationMessages.SALE_CREATION_ERROR,
                        Registered = false
                    };
                }
            }
        }

        public RegisterSaleResponse DeleteSale(int saleId)
        {
            using (var transaction = _repository.Database.BeginTransaction())
            {
                try
                {
                    Sale sale = _repository.Sale.Find(saleId);
                    if (sale == null)
                    {
                        return new RegisterSaleResponse
                        {
                            SaleId = saleId.ToString(),
                            Message = ValidationMessages.SALE_NOT_FOUND,
                            Registered = false
                        };
                    }

                    var housing = _repository.Housing.FirstOrDefault(h => h.HousingId == sale.HousingId);
                    if (housing != null)
                    {
                        housing.Status = Enum.GetName(typeof(HousingStatusEnum), HousingStatusEnum.Available);
                        _repository.Housing.AddOrUpdate(housing);
                        _repository.SaveChanges();
                    }

                    _repository.Sale.Remove(sale);
                    _repository.SaveChanges();
                    transaction.Commit();

                    return new RegisterSaleResponse
                    {
                        SaleId = saleId.ToString(),
                        Message = ValidationMessages.SALE_DELETED,
                        Registered = true
                    };
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return new RegisterSaleResponse
                    {
                        SaleId = saleId.ToString(),
                        Message = ValidationMessages.SALE_DELETION_ERROR,
                        Registered = false
                    };
                }
            }
        }



    }
}