namespace MockBank.Domain.Entities.Berkeleys
{
    public class Bank:BaseDomainEntity
    {
        public string name { get; set; }
        public string transit_number { get; set; }
        public string institution_number { get; set; }
    }
}