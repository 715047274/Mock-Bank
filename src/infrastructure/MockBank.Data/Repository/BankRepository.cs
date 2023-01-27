using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Data
{
    
    public class BankRepository: RepositoryBase, IBankRepository
    {
        #region sql query

        public string InsertCommand =>
            @"INSERT INTO berkeley_address ( address1, address2, city, post_code, state,country, updated_at)
        values (@address1, @address2, @city, @post_code, @state, @country, datetime(@updated_at));SELECT last_insert_rowid();";

        public string UpdateByIdCommand =>
            @"UPDATE berkeley_address set address1=@address1, address2=@address2, city=@city, @post_code=post_code, state=@state, country=@country
        where id = @id";

        public string DeleteByIdCommand => @"Delete berkeley_address By id=@id";

        public string SelectQueryById =>
            @"select id, address1, address2, city, post_code, state, country , created_at, updated_at from berkeley_address where id = @id";

        public string SelectAllQuery =>
            @"select id, address1, address2, city, post_code, state, country , created_at, updated_at from berkeley_address";

        #endregion
        public BankRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public Task<Bank> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Bank>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> AddAsync(Bank entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateAsync(Bank entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}