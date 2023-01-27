using System.Threading.Tasks;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Application.IRepository
{
    public interface IProgramRepository: IGenericRepository<Program>
    {

        public Task<bool> CheckActiveProgramById(int id);
    }
}