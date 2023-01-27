using System;
using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using MockBank.Application.IRepository;

namespace MockBank.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection _connection;
        private IDbTransaction _transaction;

        private bool _disposed;
        /**** Repository *****/

        private IAddressRepository _addressRepo;
        private ICardholderRepository _cardholderRepo;
        private IAccountRepository _accountRepo;
        private ITransactionRepository _transactionRepo;
        private ICardRepository _cardRepo;
        private IProcessorEventRepository _processorRepo;
        private IProgramRepository _programRepo;
        public UnitOfWork(IConfiguration configuration)
        {
            _connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection"));
            // transcation pre setup
            try
            {
                Console.WriteLine($"[INFO]::{DateTime.Now.ToString()}--- Dapper Connection path {configuration.GetConnectionString("DefaultConnection")}");
                _connection.Open();
                _transaction = _connection.BeginTransaction();
            }
            catch (Exception error)
            {
                throw new SystemException($"[ERROR]::{DateTime.Now.ToString()}--- Dapper Connection Can't be found Error: {error}");
            }
        }

        public IAddressRepository AddressRepository =>
            _addressRepo ?? (_addressRepo = new AddressRepository(_transaction));

        public ICardholderRepository CardholderRepository =>
            _cardholderRepo ?? (_cardholderRepo = new CardHolderRepository(_transaction));

        public IAccountRepository AccountRepository =>
            _accountRepo ?? (_accountRepo = new AccountRepository(_transaction));
            
        public ITransactionRepository TransactionRepository => 
            _transactionRepo ?? (_transactionRepo = new TransactionRepository(_transaction));

        public ICardRepository CardRepository => _cardRepo ?? (_cardRepo = new CardRepository(_transaction));

        public IProcessorEventRepository ProcessorEventRepository =>
            _processorRepo ?? (_processorRepo = new ProcessorEventRepository(_transaction));

        public IProgramRepository ProgramRepository => _programRepo ?? (_programRepo = new ProgramRepository(_transaction));
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Complete()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _addressRepo = null;
            _cardholderRepo = null;
            _cardRepo = null;
            _accountRepo = null;
            _transactionRepo = null;
            _processorRepo = null;
            _programRepo = null;
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }

                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }

                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}