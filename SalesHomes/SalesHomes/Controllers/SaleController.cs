using Microsoft.AspNetCore.Mvc;
using SalesHomes.DTOs;
using SalesHomes.Models;
using SalesHomes.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace SalesHomes.Controllers
{
    [RoutePrefix("api/sale")]
    public class SaleController : ApiController
    {
        private readonly SaleService _saleService;
        public SaleController(SaleService saleService)
        {
            _saleService = saleService;
        }
        [HttpGet]
        [Route("get-all-sales")]
        public IHttpActionResult GetAllSales()
        {
            try
            {
                List<SaleResponse> sales = _saleService.GetAllSales();
                return Content(HttpStatusCode.Accepted, sales);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("register-sale")]
        public IHttpActionResult RegisterSale(Sale sale)
        {
            return Ok(_saleService.RegisterSale(sale));
        }

        [HttpDelete]
        [Route("delete-sale")]
        public IHttpActionResult DeleteSale([FromQuery] int saleId)
        {
            RegisterSaleResponse response = _saleService.DeleteSale(saleId);
            if (!response.Registered)
            {
                return Content(HttpStatusCode.NotFound, response);
            }
            return Ok(response);
        }
    }
}