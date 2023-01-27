using System;
using System.Threading.Tasks;

namespace MockBank.Application.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository AddressRepository { get; }
        ICardholderRepository CardholderRepository { get; }
        IAccountRepository AccountRepository { get; }
        ICardRepository CardRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        IProcessorEventRepository ProcessorEventRepository { get; }
        IProgramRepository ProgramRepository { get; }
        void Complete();
    }
}