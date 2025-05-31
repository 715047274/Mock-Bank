using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JetBrains.Annotations;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Data
{
    public class TransactionRepository : RepositoryBase, ITransactionRepository
    {
        #region sql query

        public string InsertCommand =>
            @"INSERT INTO berkeley_transaction ( account_id, external_tag, idempotency_key, transaction_amount, transaction_currency, type_id, merchant_id, processor_reference_id, updated_at)
        values (CAST(@account_id as INT), @external_tag, @idempotency_key, @transaction_amount, @transaction_currency, CAST(@type_id as INT), CAST(@merchant_id as INT), CAST(@processor_reference_id as INT), datetime(@updated_at));SELECT last_insert_rowid();";

        public string UpdateByIdCommand =>
            @"UPDATE berkeley_address set address1=@address1, address2=@address2, city=@city, @post_code=postal_code, state=@state, country=@country
        where id = @id";

        public string DeleteByIdCommand => @"Delete berkeley_address By id=@id";

        public string SelectQueryById =>
            @"SELECT t.id, t.account_id as account_id, t.external_tag, t.transaction_amount, transaction_currency, t.idempotency_key, t.timestamp, t.created_at, t.updated_at,
              t.merchant_id, m.id, m.mcc, m.mcc_description,m.name,m.city, m.country,  
              t.type_id,code.id, code.code_type,
              t.processor_reference_id,p.id, p.reference_id, p.message, p.status, p.action_name,
              t.account_id, account.id, account.cardholder_id, account.program_id 
              FROM berkeley_transaction as t
              inner join berkeley_merchant as m on t.merchant_id == m.id
              inner join berkeley_transaction_code as code on t.type_id == code.id
              inner join berkeley_processor_event p on t.processor_reference_id = p.id
              inner join berkeley_account account on t.account_id = account.id
              where t.id == @id
              ";

        public string SelectAllQuery =>
            @"select id, address1, address2, city, postal_code, state, country , created_at, updated_at from berkeley_address";


        // Transaction history with pageNumber, limit, startDate and endDate
        public string SelectAllQueryWithLimitOffsetByAccountId => @"
              SELECT t.id, t.account_id, t.external_tag, t.transaction_amount, transaction_currency, t.idempotency_key, t.timestamp, t.created_at, t.updated_at,
              t.merchant_id, m.id, m.mcc, m.mcc_description,m.name,m.city, m.country,  
              t.type_id, code.id, code.code_type,code.description, code.transaction_sign,
              t.processor_reference_id,p.id, p.reference_id, p.message, p.status, p.action_name
              FROM berkeley_transaction as t
              inner join berkeley_merchant as m on t.merchant_id == m.id
              inner join berkeley_transaction_code as code on t.type_id == code.id
              inner join berkeley_processor_event p on t.processor_reference_id = p.id
              Where t.account_id == @id  and date(t.created_at) >= @startDate and date(t.created_at) <= @endDate
              ORDER BY t.id
              LIMIT @limit OFFSET @offset
              ";

        // List Value loads by ProgramId and ExternalTag
        public string SelectAllQueryWithExternalTagByProgramId => @"
        SELECT t.id, t.account_id as account_id, t.external_tag, t.transaction_amount, transaction_currency, t.idempotency_key, t.timestamp, t.created_at, t.updated_at,
        t.merchant_id, m.id, m.mcc, m.mcc_description,m.name,m.city, m.country,
        t.type_id, code.id, code.code_type, code.description,code.transaction_sign,
        t.processor_reference_id,p.id, p.reference_id, p.message, p.status, p.action_name,
        t.account_id, account.id, account.cardholder_id, account.program_id
            FROM berkeley_transaction as t
            left join berkeley_merchant as m on t.merchant_id == m.id
            left join berkeley_transaction_code as code on t.type_id == code.id
            left join berkeley_processor_event p on t.processor_reference_id = p.id
            left join berkeley_account account on t.account_id = account.id
            Where account.program_id =@id and external_tag =@externalTag
        ORDER BY t.id
            LIMIT @limit offset @offset";
 
      public string SelectCountWithExternalTagByProgramId => @"
        SELECT count(*) as count
            FROM berkeley_transaction as t
            left join berkeley_account account on t.account_id = account.id
            Where account.program_id =@id and external_tag =@externalTag
        ";
       // account details 
      public string SelectTransactionsByAccountId => @" SELECT t.id, t.account_id, t.external_tag, t.transaction_amount, transaction_currency, t.idempotency_key, t.timestamp, t.created_at, t.updated_at,
              t.merchant_id, m.id, m.mcc, m.mcc_description,m.name,m.city, m.country,  
              t.type_id, code.id, code.code_type,code.description, code.transaction_sign,
              t.processor_reference_id,p.id, p.reference_id, p.message, p.status, p.action_name
              FROM berkeley_transaction as t
              inner join berkeley_merchant as m on t.merchant_id == m.id
              inner join berkeley_transaction_code as code on t.type_id == code.id
              inner join berkeley_processor_event p on t.processor_reference_id = p.id
              Where t.account_id == @id";
      
      
        #endregion

        public TransactionRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            // throw new System.NotImplementedException();
            var transcationData =
                await Connection.QueryAsync<Transaction, Merchant, TransactionCode, ProcessorEvent, Account,Transaction>(
                    SelectQueryById,
                    (transaction, merchant, type, evt, account) =>
                    {
                        transaction.merchant = merchant;
                        transaction.transactionCode = type;
                        transaction.processorEvent = evt;
                        transaction.program_id = account.program_id;
                        transaction.cardholder_id = account.cardholder_id;
                        return transaction;
                    }, splitOn: "id, merchant_id, type_id, processor_reference_id, account_id", param: new {id});

            return transcationData.FirstOrDefault();
        }

        public Task<IReadOnlyList<Transaction>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Transaction entity)
        {
            //throw new System.NotImplementedException();
            var transactionId = await Connection.ExecuteScalarAsync<int>(InsertCommand, entity);
            return transactionId;
        }

        public Task<int> UpdateAsync(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Transaction>> GetTransactionsByAccountId(int accountId,   string? startDate,
          string? endDate, int limit = 50, int offset = 0)
        {
            var transcations =
                await Connection.QueryAsync<Transaction, Merchant, TransactionCode, ProcessorEvent, Transaction>(
                    SelectAllQueryWithLimitOffsetByAccountId,
                    (transaction, merchant, type, evt) =>
                    {
                        transaction.merchant = merchant;
                        transaction.transactionCode = type;
                        transaction.processorEvent = evt;
                        return transaction;
                    }, splitOn: "id, merchant_id, type_id, processor_reference_id",
                    param: new {id=accountId, startDate, endDate, limit, offset});
            return transcations.ToList();
        }

        public async Task<List<Transaction>> GetListValueLoadsByProgramId(int programId, string externalTag,
            int limit = 50, int offset = 0)
        {
            var transcations =
                await Connection.QueryAsync<Transaction, Merchant, TransactionCode, ProcessorEvent, Account, Transaction>(
                    SelectAllQueryWithExternalTagByProgramId,
                    (transaction, merchant, type, evt, account) =>
                    {
                        transaction.merchant = merchant;
                        transaction.transactionCode = type;
                        transaction.processorEvent = evt;
                        transaction.program_id = account.program_id;
                        transaction.cardholder_id = account.cardholder_id;
                        return transaction;
                    }, splitOn: "id, merchant_id, type_id, processor_reference_id, account_id",
                    param: new {id = programId, externalTag, limit, offset});
            return transcations.ToList();
        }
        
        public async Task<int> GetListValueLoadsCountByProgramId(int programId, string externalTag)
        {
            var count =
                await Connection.ExecuteScalarAsync<int>(
                    SelectCountWithExternalTagByProgramId,
                    param: new {id = programId, externalTag});
            return count;
        }

        public async Task<List<Transaction>> GetTransactionsByAccountId(int accountId)
        {
            var transcations =
                await Connection.QueryAsync<Transaction, Merchant, TransactionCode, ProcessorEvent, Transaction>(
                    SelectTransactionsByAccountId,
                    (transaction, merchant, type, evt) =>
                    {
                        transaction.merchant = merchant;
                        transaction.transactionCode = type;
                        transaction.processorEvent = evt;
                        return transaction;
                    }, splitOn: "id, merchant_id, type_id, processor_reference_id",
                    param: new {id=accountId});
            return transcations.ToList();
        }
    }
}