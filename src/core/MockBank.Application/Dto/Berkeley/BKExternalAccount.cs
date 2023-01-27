using System;
using System.Collections.Generic;
using MockBank.Application.Dto;

namespace ConnectorContracts.Models.CentralBank
{
    public class BKExternalAccountRequest
    {
        public string name { get; set; }
        public string type { get; set; }
        public string vanity_identifier { get; set; }
    }

    public class BKExternalAccountResponse
    {
        public BKExternalAccount data { get; set; }
    }

    public class BKExternalAccount : ErrorResponse
    {
        public int cardholder_id { get; set; }
        public string email { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class BKExternalAccounts
    {
        public IList<BKExternalAccount> data { get; set; }
    }
}