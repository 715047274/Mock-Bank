using System;
using System.Collections.Generic;
using System.Text;

namespace MockBank.Application.Dto.Berkeley
{
    public class BKEFTAccountHolders
    {
        public AccountHolder data { get; set; }
    }
    public class Account
    {
        public string account_holder_name { get; set; }
        public string account_number { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public string id { get; set; }
        public string institution_number { get; set; }
        public string last_four_digits { get; set; }
        public string network_type { get; set; }
        public string risk_level { get; set; }
        public string routing_number { get; set; }
        public string status { get; set; }
        public string transit_number { get; set; }
        public string type { get; set; }
    }

    public class Address
    {
        public object city { get; set; }
        public object country { get; set; }
        public object line_1 { get; set; }
        public object line_2 { get; set; }
        public object line_3 { get; set; }
        public object line_4 { get; set; }
        public object postal_code { get; set; }
        public object region { get; set; }
    }

    public class AccountHolder
    {
        public IList<Account> accounts { get; set; }
        public Address address { get; set; }
        public object default_account_id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public int id { get; set; }
        public string last_name { get; set; }
        public object phone { get; set; }
        public string type { get; set; }
    }
}
