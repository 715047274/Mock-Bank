using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Data
{
    public class ProcessorEventRepository : RepositoryBase, IProcessorEventRepository
    {
        #region sql query

        public string InsertCommand =>
            @"INSERT INTO berkeley_processor_event ( message, status, action_name, reference_id, delay_millisecond,updated_at)
        values (@message, @status, @action_name, @reference_id, CAST(@delay_millisecond as INT), datetime(@updated_at));SELECT last_insert_rowid();";

        public string UpdateByIdCommand =>
            @"UPDATE berkeley_address set address1=@address1, address2=@address2, city=@city, @postal_code=postal_code, state=@state, country=@country
        where id = @id";

        public string DeleteByIdCommand => @"Delete berkeley_address By id=@id";

        public string SelectQueryById =>
            @"select id, message , status, action_name, delay_millisecond, reference_id, created_at, updated_at from berkeley_processor_event where id = @id";

        public string SelectAllQuery =>
            @"select id, address1, address2, city, postal_code, state, country , created_at, updated_at from berkeley_address";


        public string UpdateProcessEvtStatusById => @"UPDATE berkeley_processor_event set status=@status, updated_at=datetime('now', 'localtime') WHERE id=@id";
        
        #endregion

        public ProcessorEventRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<ProcessorEvent> GetByIdAsync(int id)
        {
            // throw new System.NotImplementedException();
            var processorEvt = await Connection.QueryAsync<ProcessorEvent>(SelectQueryById, new {id});
            return processorEvt.FirstOrDefault();
        }

        public Task<IReadOnlyList<ProcessorEvent>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> AddAsync(ProcessorEvent entity)
        {
           // throw new System.NotImplementedException();
           var processId = await Connection.ExecuteScalarAsync<int>(InsertCommand, entity);
           return processId;
        }

        public Task<int> UpdateAsync(ProcessorEvent entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateEvtStatus(int id, string status)
        {
            var isChanged = await Connection.ExecuteAsync(UpdateProcessEvtStatusById, new {id=id, status=status});
            return isChanged != 0;
        }

    }
}