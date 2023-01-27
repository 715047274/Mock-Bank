using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Data.Configurations.Entities
{
    public class CardHolderConfiguration : IEntityTypeConfiguration<CardHolder>
    {
        public void Configure(EntityTypeBuilder<CardHolder> builder)
        {
        }
        
    }
}