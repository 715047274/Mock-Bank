using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Data
{
    public class CardRepository : RepositoryBase , ICardRepository
    {
        #region sql query
        
        public string InsertCommand =>
            @"INSERT INTO berkeley_card ( card_number, expiry_year, expiry_month, cvv, account_id, updated_at, activation_date, registration_date,order_shipping_method_id, order_tracking_number,order_status,shipping_date)
        values (@card_number, @expiry_year, @expiry_month, @cvv, CAST(@account_id as INT) ,datetime(@updated_at), date(@activation_date), date(@registration_date), CAST(@order_shipping_method_id as INT),@order_tracking_number, @order_status, date(@shipping_date));SELECT last_insert_rowid();";

        public string UpdateByIdCommand =>
            @"UPDATE berkeley_card set card_number=@card_number, expiry_year=@expiry_year, expiry_month=@expiry_month, @cvv=cvv, activation_date=date(@activation_date), order_shipping_method_id=@order_shipping_method_id,
             order_status = @order_status, order_tracking_number= @order_tracking_number, registration_date = date(@registration_date), shipping_date = date(@shipping_date), status_code= @status_code, updated_at=datetime(@updated_at)
        where id = @id";

        public string DeleteByIdCommand => @"Delete berkeley_address By id=@id";

        public string SelectQueryById =>
            @"select id, account_id, card_number, expiry_year, expiry_month, cvv, status_code, activation_date , order_shipping_method_id,order_status,order_tracking_number, registration_date, shipping_date, updated_at from berkeley_card where id = @id";

        public string SelectAllQuery =>
            @"select id, address1, address2, city, postal_code, state, country , created_at, updated_at from berkeley_address";

        #endregion

        public string UpdateCardStatusByIdCommand => @"UPDATE berkeley_card SET status_code=@status_code, 
                activation_date=date(@activation_date), updated_at= datetime(@updated_at) WHERE id = @id";
        public CardRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public Task<Card> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<IReadOnlyList<Card>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }
        public async Task<int> AddAsync(Card entity)
        {
            // throw new System.NotImplementedException();
            var cardId = await Connection.ExecuteScalarAsync<int>(InsertCommand, entity);
            return cardId;
        }
        public Task<int> UpdateAsync(Card entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateCardStatus(Card entity)
        {
            var cardParams = new
            {
                id = entity.id,
                status_code = entity.status_code,
                activation_date = entity.activation_date,
                updated_at = entity.updated_at,
            };
            var isChanged = await Connection.ExecuteAsync(UpdateCardStatusByIdCommand, cardParams);
            return isChanged != 0;
        }
    }
}