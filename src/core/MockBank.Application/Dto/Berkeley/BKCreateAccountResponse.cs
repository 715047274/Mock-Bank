using System;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Application.Dto.Berkeley
{
    // Cardholder response
    public class BKCreateAccountResponse
    {
        public BKCreateAccountData data { get; set; }
    }

    public class BKCreateAccountData
    {
        public BankInfo bank_details { get; set; }

        public string? company_id { get; set; }
        public DateTime created_at { get; set; }
        public string external_tag { get; set; }
        public int id { get; set; }
        public string primary_processor_reference { get; set; }
        public string username { get; set; }
        public BkLoadStatus value_load_result { get; set; } = new BkLoadStatus();
    }

    public class BkLoadStatus
    {
        public string code { get; set; } = "success";
        public string message { get; set; } = "";
    }
}