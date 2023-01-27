using System;
using System.Collections.Generic;

// using System.ComponentModel.DataAnnotations.Schema;

namespace MockBank.Domain.Entities.Berkeleys
{
    // [Table("berkeley_cardholder")]
    public class CardHolder : BaseDomainEntity
    {
        public string first_name { get; set; }
        public string? middle_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string? emboss_line { get; set; }
        
        public string date_of_birth { get; set; }
        public string? sin { get; set; }
        public string phone { get; set; }

        public int address_id { get; set; }
        public int shipping_method_id { get; set; }

        public virtual string? address1()
        {
            return shipping_address.address1;
        }

        public virtual string? address2()
        {
            return shipping_address.address2;
        }

        public virtual string? city()
        {
            return shipping_address.city;
        }

        public virtual string? state()
        {
            return shipping_address.state;
        }

        public virtual string? postal_code()
        {
            return shipping_address.postal_code;
        }

        public virtual string? country()
        {
            return shipping_address.country;
        }

        public virtual string shipping_method => ShippingMethod.method_name;
        
        public virtual ShippingMethod? ShippingMethod { get; set; }
        public virtual Address? shipping_address { get; set; }
        public virtual List<Account> accounts { get; set; }
    }
}