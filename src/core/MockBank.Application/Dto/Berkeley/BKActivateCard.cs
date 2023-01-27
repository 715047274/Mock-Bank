using ConnectorContracts.Models.CentralBank;

namespace MockBank.Application.Dto.Berkeley
{
    public class BKActivateCard 
    {
        public int id { get; set; }
        public string last_four_digits { get; set; }
        public string expiry_month { get; set; }
        public string expiry_year { get; set; }
        public string? cvv { get; set; }
    }
}