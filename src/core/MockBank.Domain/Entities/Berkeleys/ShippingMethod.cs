// using System.ComponentModel.DataAnnotations.Schema;

namespace MockBank.Domain.Entities.Berkeleys
{
    public class ShippingMethod :BaseDomainEntity
    {
        public string method_name { get; set; }
        // [ForeignKey(nameof(CardHolder))]
        public int card_holder_id { get; set; }
    }
}