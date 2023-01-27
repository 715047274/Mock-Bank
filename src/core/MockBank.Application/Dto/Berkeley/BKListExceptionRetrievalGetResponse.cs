using System;
using System.Collections.Generic;
using System.Text;

namespace MockBank.Application.Dto.Berkeley
{
    public class BKListExceptionRetrievalGetResponse
    {
        public BKListExceptionRetrievalGetTransactions data { get; set; }
    }
    public class BKListExceptionRetrievalGetTransactions
    {
        public IList<BKListExceptionRetrievalGetTransaction> Transactions { get; set; }
    }
    public class BKListExceptionRetrievalGetTransaction
    {
        public string billing_amount { get; set; }
        public string billing_currency { get; set; }
        public string description { get; set; }
        public BKMerchant merchant { get; set; }
        public string processor_reference { get; set; }
        public string timestamp { get; set; }
        public string transaction_amount { get; set; }
        public string transaction_currency { get; set; }
        public string type { get; set; }
    }
}
