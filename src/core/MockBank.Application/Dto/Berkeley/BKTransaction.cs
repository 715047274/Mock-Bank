namespace MockBank.Application.Dto.Berkeley
{
    public class BKTransaction
    {
        public string billing_amount { get; set; }
        public string billing_currency { get; set; }
        public string description { get; set; }
        public string timestamp { get; set; }
        public string transaction_amount { get; set; }
        public string transaction_currency { get; set; }
        public string type { get; set; }
    }
}
