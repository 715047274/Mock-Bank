namespace MockBank.Application.Dto.Berkeley
{
    public class BKBankTransferResponse
    {
        public int account_id { get; set; }
        public int amount { get; set; }
        public int cardholder_id { get; set; }
        public int external_account_id { get; set; }
        public string external_tag { get; set; }
        public int id { get; set; }
        public string message { get; set; }
        public int program_id { get; set; }
        public string status { get; set; }
        public string transaction_id { get; set; }
    }

}