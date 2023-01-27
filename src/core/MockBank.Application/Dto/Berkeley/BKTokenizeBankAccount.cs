using MockBank.Application.Dto;

namespace ConnectorContracts.Models.CentralBank
{
    public class BKTokenizeBankAccountRequest
    {
        public BKTokenizeBankAccountRequestData bank_account { get; set; }
    }

    public class BKTokenizeBankAccountRequestData
    {
        public string account_number { get; set; }
        public string account_holder_name { get; set; }
        public string transit_number { get; set; }
        public string institution_number { get; set; }
        public string currency { get; set; }
        public string country { get; set; }
    }

    public class BKTokenizeBankAccountResponse
    {
        public BKTokenizeBankAccountResponseData data { get; set; }
    }

    public class BKTokenizeBankAccountResponseData : ErrorResponse
    {
        public string account_number { get; set; }
        public string account_holder_name { get; set; }
        public string transit_number { get; set; }
        public string institution_number { get; set; }
        public string routing_number { get; set; }
        public string currency { get; set; }
        public string country { get; set; }
        public string last_four_digits { get; set; }
        public string risk_level { get; set; }
        public string token { get; set; }
        public string type { get; set; }
    }
}