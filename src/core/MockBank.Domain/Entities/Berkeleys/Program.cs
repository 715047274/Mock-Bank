// using System.ComponentModel.DataAnnotations.Schema;

using System.Collections.Generic;

namespace MockBank.Domain.Entities.Berkeleys
{
    //[Table("berkeley_program")]
    public class Program: BaseDomainEntity
    {
        public string name { get; set; }
        public string program_type { get; set; }
        public string? master_funding_account_number { get; set; }
        public string status { get; set; }
        public int currency { get; set; }
        public virtual List<Account> Accounts { get; set; } = new List<Account>();
    }
}