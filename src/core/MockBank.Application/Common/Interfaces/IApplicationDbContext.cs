using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Application.Configurations.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        
        /***************
         * DB instance configuration
         ***************/
        // DbSet<Card> Card { get; set; }
        // DbSet<Address> Addresse { get; set; }
        // DbSet<Account> Account { get; set; }
        // DbSet<CardHolder> CardHolder { get; set; }
        // DbSet<Merchant> Merchant { get; set; }
        // DbSet<Transaction> Transaction { get; set; }
        //
        // DbSet<ProcessorEvent> ProcessorEvent { get; set; }
        DbSet<TransactionCode> TransactionCodes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}