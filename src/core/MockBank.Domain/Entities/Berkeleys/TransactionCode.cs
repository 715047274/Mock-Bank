// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

namespace MockBank.Domain.Entities.Berkeleys
{
    // [Table("berkeley_transcation_code")]
    public class TransactionCode
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // [Key, Column(Order = 0)]
        public int id { get; set; }
        public string code_type { get; set; }
        public string description { get; set; }
                      
        public string transaction_sign { get; set; }
    }
}