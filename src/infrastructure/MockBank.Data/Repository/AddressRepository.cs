using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Data
{
    public class AddressRepository : RepositoryBase, IAddressRepository
    {
        #region sql query

        public string InsertCommand =>
            @"INSERT INTO berkeley_address ( address1, address2, city, postal_code, state,country, updated_at)
        values (@address1, @address2, @city, @postal_code, @state, @country, datetime(@updated_at));SELECT last_insert_rowid();";

        public string UpdateByIdCommand =>
            @"UPDATE berkeley_address set address1=@address1, address2=@address2, city=@city, postal_code=@postal_code, state=@state, country=@country
        where id = @id";

        public string DeleteByIdCommand => @"DELETE FROM berkeley_address where id=@id";

        public string SelectQueryById =>
            @"select id, address1, address2, city, postal_code, state, country , created_at, updated_at from berkeley_address where id = @id";

        public string SelectAllQuery =>
            @"select id, address1, address2, city, postal_code, state, country , created_at, updated_at from berkeley_address";

        #endregion
        
        public AddressRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            //throw new System.NotImplementedException();
            var result = await Connection.QueryFirstAsync<Address>(
                SelectQueryById, new {id=id});
            return result;
        }

        public async Task<IReadOnlyList<Address>> GetAllAsync()
        {
            var addressList = await Connection.QueryAsync<Address>(SelectAllQuery);
            // throw new NotImplementedException();
            return addressList.ToList();
        }

        public async Task<int> AddAsync(Address entity)
        {
            var newAddressId =
                await Connection.ExecuteScalarAsync<int>(InsertCommand, entity, Transaction, null, CommandType.Text);
            return newAddressId;
        }

        public async Task<int> UpdateAsync(Address entity)
        {
            var modifiedRow = await Connection.ExecuteAsync(UpdateByIdCommand, entity);
            return modifiedRow;
        }

        public async  Task<int> DeleteAsync(int id)
        {
            // throw new NotImplementedException();
            var deletedId = await Connection.ExecuteAsync(DeleteByIdCommand, new {id=id});
            return deletedId;
        }
    }
}