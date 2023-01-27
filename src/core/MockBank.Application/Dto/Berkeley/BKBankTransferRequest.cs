namespace MockBank.Application.Dto.Berkeley
{
    public class BKBankTransferRequest
    {
        public int account_id { get; set; }
        public int external_account_id { get; set; }
        public int amount { get; set; }
        public string transfer_type { get; set; }
        public string external_tag { get; set; }
        public string message { get; set; }
    }
}