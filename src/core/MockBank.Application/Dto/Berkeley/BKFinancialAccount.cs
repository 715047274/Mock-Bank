using MockBank.Application.Dto;

namespace ConnectorContracts.Models.CentralBank
{
    public class BKFinancialAccountRequest
    {
        public string token { get; set; }
    }

    public class BKFinancialAccountResponse
    {
        public BKFinancialAccount data { get; set; }
    }

    public class BKFinancialAccount : ErrorResponse
    {
        public int account_holder_id { get; set; }
        public string account_holder_name { get; set; }
        public string account_number { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public string id { get; set; }
        public string institution_number { get; set; }
        public string last_four_digits { get; set; }
        public string network_type { get; set; }
        public string risk_level { get; set; }
        public string routing_number { get; set; }
        public string status { get; set; }
        public string transit_number { get; set; }
        public string type { get; set; }
    }
}