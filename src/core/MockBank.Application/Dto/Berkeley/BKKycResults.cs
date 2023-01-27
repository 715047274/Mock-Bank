using System;
using System.Collections.Generic;
using System.Text;

namespace MockBank.Application.Dto.Berkeley
{
  
    public class KycResult
    {
        public string code { get; set; }
        public List<KycResultCode> kyc_result_codes { get; set; }
        public string kyc_transaction_id { get; set; }
        public string message { get; set; }
    }

    public class BKKycResult
    {
        public object cardholder { get; set; }
        public string code { get; set; }
        public string company_id { get; set; }
        public DateTime created_at { get; set; }
        public int id { get; set; }
        public string kyc_transaction_id { get; set; }
        public KycResult result { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class BKKycResults
    {
       public BKKycResult data { get; set; }
    }
}
