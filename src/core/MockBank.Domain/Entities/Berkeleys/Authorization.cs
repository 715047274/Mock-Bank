//using System.ComponentModel.DataAnnotations.Schema;

using System;

namespace MockBank.Domain.Entities.Berkeleys
{
    //[Table("berkeley_authorization")]
    public class Authorization : BaseDomainEntity
    {
        public string description { get; set; }
        public string billing_amount { get; set; }
        public string billing_currency { get; set; }
        public string external_tag { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public DateTime timestamp { get; set; }
        public string transaction_amount { get; set; }
        public string transaction_currency { get; set; }
        public int account_id { get; set; }

    }
}