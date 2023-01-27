using System;
// using System.Runtime.CompilerServices;

namespace MockBank.Domain.Entities.Berkeleys
{
    public abstract class BaseDomainEntity {
        public int id { get; set; }
        public DateTime created_at { get; set; } // default value handle with the DB Creation
        public DateTime updated_at { get; set; }= DateTime.Now; // need to be updated each update record
    }
}