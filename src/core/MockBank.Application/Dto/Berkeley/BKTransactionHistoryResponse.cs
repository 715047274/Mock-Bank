using System;
using System.Collections.Generic;

namespace MockBank.Application.Dto.Berkeley
{
    public class BKTransactionHistoryResponse  
    {
        public IList<BKTransactionHistoryData> data { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
    }

    public class BKTransactionHistoryData
    {
        public string billing_amount { get; set; }
        public string billing_currency { get; set; }
        public string description { get; set; }
        public BKMerchant merchant { get; set; }
        public DateTime timestamp { get; set; }
        public string transaction_amount { get; set; }
        public string transaction_currency { get; set; }
        public string type { get; set; }
        public bool is_authorization { get; set; }
    }

    public class BKMerchant
    {
        public string city { get; set; }
        public string country { get; set; }
        public string mcc { get; set; }
        public string mcc_description { get; set; }
        public string name { get; set; }
    }

}