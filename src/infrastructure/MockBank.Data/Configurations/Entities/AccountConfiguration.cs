using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Data.Configurations.Entities
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            // throw new System.NotImplementedException();
            /*
             * builder.Ignore(e=>e.DomainEvents);
             * builder.Property(t=>t.Title).HasMaxLength(200).IsRequired();
             * builder.HasData(
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10, 
                    Name = "Vacation"
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 12,
                    Name = "Sick"
                }
            );
            */
        }
    }
}