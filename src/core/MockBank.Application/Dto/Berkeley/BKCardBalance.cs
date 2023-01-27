namespace MockBank.Application.Dto.Berkeley
{
    public class BKAccountBalanceResponse
    {
        public BKAccountBalance data { get; set; }
    }

    public class BKAccountBalance
    {
        public string available_balance { get; set; }
        public string balance { get; set; }
        public string currency { get; set; }
        public string settled_balance { get; set; }
    }
}