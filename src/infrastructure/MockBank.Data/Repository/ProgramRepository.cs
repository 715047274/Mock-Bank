using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Data
{
    public class ProgramRepository: RepositoryBase, IProgramRepository
    {
      

        

         #region sql query

        public string InsertCommand =>
            @"INSERT INTO berkeley_address ( address1, address2, city, postal_code, state,country, updated_at)
        values (@address1, @address2, @city, @post_code, @state, @country, datetime(@updated_at));SELECT last_insert_rowid();";

        public string UpdateByIdCommand =>
            @"UPDATE berkeley_address set address1=@address1, address2=@address2, city=@city, @post_code=postal_code, state=@state, country=@country
        where id = @id";

        public string DeleteByIdCommand => @"Delete berkeley_address By id=@id";

        public string SelectQueryById =>
            @"select id, address1, address2, city, postal_code, state, country , created_at, updated_at from berkeley_address where id = @id";

        public string SelectAllQuery =>
            @"select id, address1, address2, city, postal_code, state, country , created_at, updated_at from berkeley_address";

        public string VerifyProgramById => @"SELECT id FROM berkeley_program WHERE status = 'active' and id =@id";
        
        #endregion
        public ProgramRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public Task<Program> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Program>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> AddAsync(Program entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateAsync(Program entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> CheckActiveProgramById(int id)
        {
            var programId = await Connection.QueryAsync<int>(VerifyProgramById, new {id});
            return programId.Count() != 0;
        }

    }
}