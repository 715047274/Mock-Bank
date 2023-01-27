using System;
// using System.ComponentModel.DataAnnotations.Schema;

namespace MockBank.Domain.Entities.Berkeleys
{
  //  [Table("berkeley_card")]
    public class Card: BaseDomainEntity
    {
        public int account_id { get; set; }
        public string card_number { get; set; }
        public string last_four_digits => card_number.Substring(card_number.Length - 4);
        public virtual string expiry_date => new DateTime(Int32.Parse(expiry_year), Int32.Parse( expiry_month),20).ToString();
        public virtual string number => card_number;
        public virtual string status => status_code;
        public string expiry_year { get; set; }
        public string expiry_month { get; set; }
        public string cvv { get; set; }
        public string status_code { get; set; }

        public DateTime activation_date { get; set; }
        public int order_shipping_method_id { get; set; }
        public string order_status { get; set; }
        public string order_tracking_number { get; set; }
        public DateTime registration_date { get; set; }
        public DateTime shipping_date { get; set; }
    }
}