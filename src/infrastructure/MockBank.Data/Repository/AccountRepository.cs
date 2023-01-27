using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Data
{
    public class AccountRepository : RepositoryBase, IAccountRepository
    {
        #region sql query

        public string InsertCommand =>
            @"INSERT INTO berkeley_account ( account_number, start_date, updated_at, cardholder_id, bank_id, program_id)
        values (@account_number, datetime(@start_date), datetime(@updated_at), CAST(@cardholder_id as INT), CAST(@bank_id as INT), CAST(@program_id as INT));SELECT last_insert_rowid();";

        public string UpdateByIdCommand =>
            @"UPDATE berkeley_address set address1=@address1, address2=@address2, city=@city, @postal_code=postal_code, state=@state, country=@country
        where id = @id";

        public string DeleteByIdCommand => @"Delete berkeley_address By id=@id";

        // Account Details 
        public string SelectQueryById =>
            @"select account.id,account.cardholder_id,account.program_id,account.account_number, account.status_code, account.balance, account.start_date, account.end_date, account.created_at, account.bank_id, 
            bank.id, bank.transit_number, bank.institution_number,
            card.account_id, card.id, card.card_number, card.expiry_year, card.expiry_month, card.cvv, card.status_code, card.activation_date, card.registration_date, card.shipping_date,card.order_status, card.order_tracking_number, card.created_at, card.updated_at
           from berkeley_account as account 
           inner join berkeley_bank as bank on bank.id == account.bank_id  
           inner join berkeley_card as card on card.account_id == account.id
           where account.id= @id
           ";

        // Cardholder Details
        public string SelectCardholderAccount =>
            @"select account.id, account.account_number, account.status_code, account.balance, account.created_at, account.start_date, account.end_date,account.cardholder_id, account.bank_id, 
            bank.id, bank.transit_number, bank.institution_number,
            card.account_id, card.id, card.card_number, card.expiry_year, card.expiry_month, card.cvv, card.status_code, card.activation_date, card.registration_date, card.shipping_date,card.order_status, card.order_tracking_number, card.created_at, card.updated_at
           from berkeley_account as account 
           inner join berkeley_bank as bank on bank.id == account.bank_id  
           inner join berkeley_card as card on card.account_id == account.id
           where account.cardholder_id= @cardholderId
           ";

        public string SelectAllQuery =>
            @"select id, address1, address2, city, postal_code, state, country , created_at, updated_at from berkeley_address";

        public string UpdateAccountBalanceQueryCommand =>
            @"UPDATE berkeley_account set balance = @balance, processor_reference_id = @processor_reference_id, 
                            updated_at=datetime(@updated_at) WHERE id = @id";

        public string UpdateAccountStatusCommand =>
            @"UPDATE berkeley_account set status_code = @status_code, updated_at = datetime(@updated_at) WHERE id = @id";


        public string SelectAccountBalanceById => @"SELECT * FROM berkeley_account WHERE id=@id";
        
        #endregion

        public AccountRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        #region Gerneric Repository

        public async Task<Account> GetByIdAsync(int id)
        {
            // throw new System.NotImplementedException();
            var account = await Connection.QueryAsync<Account, Bank, Card, Account>(SelectQueryById,
                (account, bank, card) =>
                {
                    account.bank = bank;
                    account.cards.Add(card);
                    return account;
                }, splitOn: "id, bank_id, account_id", param: new {id});
            return account.FirstOrDefault();
        }

        public async Task<List<Account>> QueryAccountByCardHolderId(int cardholderId)
        {
            var account = await Connection.QueryAsync<Account, Bank, Card, Account>(SelectCardholderAccount,
                (account, bank, card) =>
                {
                    account.bank = bank;
                    account.cards.Add(card);
                    return account;
                }, splitOn: "id, bank_id, account_id", param: new {cardholderId});
            return account.ToList();
        }

        public Task<IReadOnlyList<Account>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Account entity)
        {
            /*
             *  Get Account information from the program Id 
             * 
             */
            // throw new System.NotImplementedException();
            var accountId =
                await Connection.ExecuteScalarAsync<int>(InsertCommand, entity, Transaction, null, CommandType.Text);
            return accountId;
        }

        public Task<int> UpdateAsync(Account entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        public async Task<bool> UpdateAccountBalance(Account entity)
        {
            var AccountParams = new
            {
                entity.id,
                entity.balance,
                entity.processor_reference_id,
                updated_at = DateTime.Now
            };
            var isChanged = await Connection.ExecuteAsync(UpdateAccountBalanceQueryCommand, AccountParams);
            return isChanged != 0;

        }

        public async Task<bool> ActiveAccountStatus(Account entity)
        {
            var AccountParams = new
            {
                entity.id,
                entity.status_code,
                updated_at = DateTime.Now
            };

            var isChanged = await Connection.ExecuteAsync(UpdateAccountStatusCommand, AccountParams);
            return isChanged != 0;
        }

        public async Task<Account> GetAccountBalanceById(int id)
        {
            var accountBalance = await Connection.QueryAsync<Account>(SelectAccountBalanceById, param: new {id});
            return accountBalance.FirstOrDefault();
        }
    }
}