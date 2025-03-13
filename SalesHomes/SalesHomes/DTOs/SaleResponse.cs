using Newtonsoft.Json;

namespace SalesHomes.DTOs
{
    public class SaleResponse
    {
        public string Agency { get; set; }
        public string NameClient { get; set; }
        public string Document { get; set; }
        public string TypeHousing { get; set; }

        public string Price { get; set; }

        public string SizeM2 { get; set; }

    }
    public class RegisterSaleResponse {
        public string SaleId { get; set; }
        public string Message { get; set; }
        public bool Registered { get; set; }

    }
}