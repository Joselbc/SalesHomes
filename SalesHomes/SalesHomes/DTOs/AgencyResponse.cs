using Newtonsoft.Json;

namespace SalesHomes.DTOs
{
    public class AgencyResponse
    {
        public string AgencyId { get; set; }
        public string Message { get; set; }
        public bool Registered { get; set; }
    }
}