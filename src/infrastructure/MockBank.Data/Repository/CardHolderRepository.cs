using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Data
{
    public class CardHolderRepository : RepositoryBase, ICardholderRepository
    {
        #region sql query

        public string InsertCommand =>
            @"INSERT INTO berkeley_cardholder ( first_name, middle_name, last_name, date_of_birth, emboss_line,phone, email, sin, shipping_method_id, address_id, updated_at)
        values (@first_name, @middle_name, @last_name, @date_of_birth, @emboss_line, @phone, @email, @sin, CAST(@shipping_method_id as INT), CAST(@address_id as INT), datetime(@updated_at));SELECT last_insert_rowid();";


        public string UpdateByIdCommand =>
            @"UPDATE berkeley_address set address1=@address1, address2=@address2, city=@city, @post_code=postal_code, state=@state, country=@country
        where id = @id";

        public string DeleteByIdCommand => @"Delete berkeley_address By id=@id";

        public string SelectQueryById =>
            @"select id, address1, address2, city, postal_code, state, country , created_at, updated_at from berkeley_address where id = @id";

        public string SelectAllQuery =>
            @"select id, address1, address2, city, postal_code, state, country , created_at, updated_at from berkeley_address";

        string SelectCardhoderDetail =
            @"SELECT u.id,u.first_name, u.middle_name, u.last_name, u.date_of_birth, u.emboss_line, u.phone, u.email, u.sin, u.created_at, u.updated_at,
              u.address_id, a.id, a.address1, a.address2, a.city, a.postal_code, a.state, a.country, a.created_at, a.updated_at,
              u.shipping_method_id, s.id, s.method_name
              from berkeley_cardholder as u 
              inner join berkeley_address as a on a.id == u.address_id
              inner join shipping_method as s on s.id == u.shipping_method_id where u.id = @id";

        #endregion

        public CardHolderRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        #region Generate method

        public async Task<CardHolder> GetByIdAsync(int id)
        {
            var cardHolders = await Connection.QueryAsync<CardHolder, Address, ShippingMethod, CardHolder>(
                SelectCardhoderDetail, (user, address, shippingMethod) =>
                {
                    user.shipping_address = address;
                    user.ShippingMethod = shippingMethod;
                    return user;
                }, splitOn: "id, address_id, shipping_method_id", param: new {id});
            return cardHolders.FirstOrDefault();
        }

        public Task<IReadOnlyList<CardHolder>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(CardHolder entity)
        {
            var cardHolderId =
                await Connection.ExecuteScalarAsync<int>(InsertCommand, entity, Transaction, null, CommandType.Text);
            return cardHolderId;
        }

        public Task<int> UpdateAsync(CardHolder entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}