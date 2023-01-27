using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MockBank.Application.Dto.Berkeley
{
    public class BKCreateCardholder : ValidatableModel
    {
        public int program_id { get; set; }
        public string external_tag { get; set; }
        //public string order_batch_id { get; set; }
        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }
        [Required]
        public string date_of_birth { get; set; }
        [Required]
        public string address1 { get; set; }
        public string address2 { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string state { get; set; }
        [Required]
        public string postal_code { get; set; }
        [Required]
        public string country { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string email { get; set; }
        public string shipping_method { get; set; }
        public BKShippingAddress shipping_address { get; set; }
    }

    public class BKShippingAddress
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
    }
}