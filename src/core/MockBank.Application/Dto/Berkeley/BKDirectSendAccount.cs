using MockBank.Application.Dto;

namespace ConnectorContracts.Models.CentralBank
{
    public class BKDirectSendAccountRequest
    {
        public string type { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }

    public class BKDirectSendAccountResponse
    {
        public BKDirectSendAccount data { get; set; }
    }

    public class BKDirectSendAccount : ErrorResponse
    {
        public string email { get; set; }
        public string type { get; set; }
        public int id { get; set; }
    }
}