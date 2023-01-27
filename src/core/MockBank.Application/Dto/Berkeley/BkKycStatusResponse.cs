using System;
using System.Collections.Generic;

namespace MockBank.Application.Dto.Berkeley
{
    public class BkKycStatusResponse
    {
        public BkKycStatusData data { get; set; }
    }

    public class BkKycStatusData
    {
        public BKCreateAccountData cardholder { get; set; }
        public string code { get; set; }
        public string company_id { get; set; }
        public int id { get; set; }
        public string kyc_transaction_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
        public string code { get; set; }
        public List<Kyc_Result_Codes> kyc_result_codes { get; set; }
        public string kyc_transaction_id { get; set; }
        public string message { get; set; }
    }

    public class Kyc_Result_Codes
    {
        public string code { get; set; }
        public string description { get; set; }
    }

}