using System.Threading.Tasks;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Application.IRepository
{
    public interface IProcessorEventRepository : IGenericRepository<ProcessorEvent>
    {
       public Task<bool> UpdateEvtStatus(int id, string status);
    }
}