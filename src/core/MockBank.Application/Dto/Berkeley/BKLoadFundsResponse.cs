using System;

namespace MockBank.Application.Dto.Berkeley
{
    public class BKLoadFundsResponse
    {
        public BKLoadFundsData data { get; set; }
    }

    public class BKLoadFundsData
    {
        public int account_id { get; set; }
        public decimal amount { get; set; }
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