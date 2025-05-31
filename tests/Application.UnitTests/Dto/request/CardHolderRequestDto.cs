using System;
using JetBrains.Annotations;
using MockBank.Domain.Entities.Berkeleys;

namespace Application.UnitTests.Dto
{
    public class CardHolderRequestDto: CardHolder
    {
        public int program_id { get; set; }
        public string first_name { get; set; }
        public string? middle_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string? emboss_line { get; set; }
        public DateTime date_of_brith { get; set; }
        public string? sin { get; set; }
        public int? shipping_method_id { get; set; }
        public AddressDto shipping_address { get; set; }
        public double? load_amount { get; set; }
        public int? linked_account_id { get; set; }
        public string? subprogram_code { get; set; }
    }
}