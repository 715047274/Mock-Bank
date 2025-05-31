using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Application.IRepository
{
    public interface ITransactionRepository:  IGenericRepository<Transaction>
    {
        public Task<List<Transaction>> GetTransactionsByAccountId(int accountId, string? startDate,
            string? endDate, int limit = 50, int offset = 0);

        public Task<List<Transaction>> GetListValueLoadsByProgramId(int programId, string externalTag,
            int limit = 50, int offset = 0);

        public Task<int> GetListValueLoadsCountByProgramId(int programId, string externalTag);
        public Task<List<Transaction>> GetTransactionsByAccountId(int accountId);
    }
}