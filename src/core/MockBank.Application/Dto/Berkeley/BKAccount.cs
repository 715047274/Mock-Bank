using System.Collections.Generic;

namespace MockBank.Application.Dto.Berkeley
{
    public class BKAccount
    {
        public BKAccountData data { get; set; }
    }

    public class BKAccountData
    {
        // authorizations is for reserving money on a card for a purchase, not relevant to ODP.
        // public string authorizations { get; set; }
        public string balance { get; set; }
        public BKBankDetails bank_details { get; set; }
        public int cardholder_id { get; set; }
        public IList<BKCard> cards { get; set; }
        public string created_at { get; set; }
        public string end_date { get; set; }
        public int id { get; set; }
        public string processor_reference { get; set; }
        public int program_id { get; set; }
        public string start_date { get; set; }
        public string status { get; set; }
        public string status_code { get; set; }
        public IList<BKTransaction> transactions { get; set; }
        public string updated_at { get; set; }
    }
}
