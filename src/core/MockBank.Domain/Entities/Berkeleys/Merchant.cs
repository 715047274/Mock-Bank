// using System.ComponentModel.DataAnnotations.Schema;

namespace MockBank.Domain.Entities.Berkeleys
{  
   //  [Table("berkeley_merchant")]
    public class Merchant :BaseDomainEntity
    {
        
        public string mcc { get; set; }
        public string mcc_description { get; set; }
        public string? name { get; set; }
        public string? city { get; set; }
        public string? country { get; set; }
    }
}