using System;
using System.Collections.Generic;

//using System.ComponentModel.DataAnnotations.Schema;
namespace MockBank.Domain.Entities.Berkeleys
{
    //[Table("berkeley_account")]
    public class Account : BaseDomainEntity
    {
        public string status_code { get; set; }
        public string balance { get; set; }
        public string account_number { get; set; }
        public string? available_balance => balance;
        public string? settled_balance => balance;
        public DateTime start_date { get; set; }
        public DateTime? end_date { get; set; }
        public virtual string status => status_code[0].ToString().ToUpper();
        public string? primary_processor_reference { get; set; }
        public string? external_account_holder_id { get; set; }
        public virtual string company_id => "81";
        public virtual string? username { get; set; }
        public string currency { get; set; } = "124";

        public string?
            processor_reference_id
        {
            get;
            set;
        } // last working processor reference ==> mapper to the processor event reference_id .

        # region foreign key

        public int cardholder_id { get; set; }
        public int bank_id { get; set; }
        public int program_id { get; set; }

        #endregion

        public virtual Bank bank { get; set; }

        public virtual BankInfo bank_details => new BankInfo
        {
            account_number = account_number,
            transit_number = bank.transit_number,
            institution_number = bank.institution_number
        };


        public virtual Program Program { get; set; }
        public virtual ProcessorEvent processor { get; set; }
        public virtual List<Card> cards { get; set; } = new List<Card>();
        public virtual List<Transaction> transactions { get; set; } = new List<Transaction>();
        public virtual List<Authorization> Authorizations { get; set; } = new List<Authorization>();
    }

    public struct BankInfo
    {
        public string account_number { get; set; }
        public string institution_number { get; set; }
        public string transit_number { get; set; }
    }
}