using JetBrains.Annotations;
using MockBank.Domain.Entities.Berkeleys;

namespace Application.UnitTests.Dto
{
    
    public class AddressDto : Address {
        public string? address1 { get; set; }
        public string? address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string post_code { get; set; }
        public string country { get; set; }
    }
}