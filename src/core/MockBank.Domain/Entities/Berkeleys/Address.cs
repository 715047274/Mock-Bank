//using System.ComponentModel.DataAnnotations.Schema;

namespace MockBank.Domain.Entities.Berkeleys
{
    //[Table("berkeley_address")]
    public class Address : BaseDomainEntity
    {
        public string address1 { get; set; }
        public string? address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public string postal_code { get; set; }
        // private string _countryCode { get; set; }

        private string _country;

        public string country
        {
            get => _country;
            set
            {
                if (value == "124")
                    _country = "Canada";
                else if (value == "840")
                    _country = "Canada";
                else
                    _country = value;
            }
        }
        //public int card_holder_id { get; set; }
    }
}