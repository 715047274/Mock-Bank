using System.Collections.Generic;
using System.Threading.Tasks;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Application.IRepository
{
    public interface IAccountRepository: IGenericRepository<Account>
    {
        // Load Values Fund Create snap shot to the account balance
        public Task<bool> UpdateAccountBalance(Account entry);
        
        // Get Cardholder Details
        public Task<List<Account>> QueryAccountByCardHolderId(int cardholderId);
       
        // activate card command
        public Task<bool> ActiveAccountStatus(Account entity);
        // account balance
        public Task<Account> GetAccountBalanceById(int id);
    }
}