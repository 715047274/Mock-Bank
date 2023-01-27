using System;
// using System.ComponentModel.DataAnnotations.Schema;

namespace MockBank.Domain.Entities.Berkeleys
{
    // [Table("berkeley_transcation")]
    public class Transaction : BaseDomainEntity
    {
        public int account_id { get; set; }
        public string external_tag { get; set; }
        public string transaction_amount { get; set; }
        public string transaction_currency { get; set; }
        public string? idempotency_key { get; set; }
        public virtual string billing_amount => transaction_amount;
       public virtual string billing_currency => transaction_currency;

        public DateTime timestamp { get; set; }
        public int merchant_id { get; set; }
        public virtual Merchant merchant { get; set; }
        public int type_id { get; set; }
        public virtual TransactionCode transactionCode { get; set; }
        public virtual string description => transactionCode.description;
        public virtual string type => transactionCode.code_type;

        public virtual string processor_reference => processorEvent.reference_id;
        public virtual decimal amount => Decimal.Parse(transaction_amount);
        public int processor_reference_id { get; set; }
        public virtual ProcessorEvent processorEvent { get; set; }
        public string message => processorEvent.message;
        public string status => processorEvent.status;
        public string load_type => processorEvent.action_name;
        public virtual int program_id { get; set; }
        public virtual int cardholder_id { get; set; }
    }
}