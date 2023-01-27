using System;
using System.Collections.Generic;

namespace MockBank.Application.Dto.Berkeley
{
    public class BKListValueLoadsResponse
    {
        public int count { get; set; }
        public IList<BKListValueLoadTransaction> data { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
      
    }
    public class BKListValueLoadTransaction
    {
        public int account_id { get; set; }
        public string amount { get; set; }
        public int cardholder_id { get; set; }
        public DateTime created_at { get; set; }
        public string external_tag { get; set; }
        public int id { get; set; }
        public string load_type { get; set; }
        public string message { get; set; }
        public string processor_reference { get; set; }
        public int program_id { get; set; }
    }
}