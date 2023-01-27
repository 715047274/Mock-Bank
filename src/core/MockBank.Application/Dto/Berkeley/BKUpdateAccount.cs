using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MockBank.Application.Dto.Berkeley
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class BKUpdateAccount
    {
        public string email { get; set; }
        public string phone { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string postal_code { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public BKShippingAddress shipping_address { get; set; }
    }
}