using System;
using System.Collections.Generic;

namespace Application.UnitTests.Dto.respond
{
    public struct CardholderCreateRespond
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public string external_tag { get; set; }
        public int primary_processor_reference { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public DateTime date_of_birth { get; set; }
        public string shipping_method { get; set; }
        public List<AccountDto> accounts { get; set; }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}