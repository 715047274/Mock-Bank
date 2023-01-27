// using System.ComponentModel.DataAnnotations.Schema;

using System;

namespace MockBank.Domain.Entities.Berkeleys
{
    public class ProcessorEvent: BaseDomainEntity
    {
        public string status { get; set; }
        public string? message { get; set; }

        public string reference_id { get; set; }
        
        public string? action_name { get; set; }
        
        public int delay_millisecond  { get; set; } // check the respond with the transcation
    
        // status: approved, declined, inprogress, awaiting_settlement, processing, completed,
        // action_message: Suspected fraud, Approved and completed successfully, no action code found, 
    }
}