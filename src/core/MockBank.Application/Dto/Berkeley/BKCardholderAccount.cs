using System.Collections.Generic;

namespace MockBank.Application.Dto.Berkeley
{
    public class BKCardholderAccount
    {
        public BKBankDetails bank_details { get; set; }
        public int cardholder_id { get; set; }
        public IList<BKCard> cards { get; set; }
        public string created_at { get; set; }
        public int id { get; set; }
        public string processor_reference { get; set; }
        public int program_id { get; set; }
        public string status { get; set; }
        public string status_code { get; set; }
        public string updated_at { get; set; }
        // public BKCardBalance card_balance { get; set; }
      
    }
}