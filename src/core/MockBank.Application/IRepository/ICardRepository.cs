using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Application.IRepository
{
    public interface ICardRepository: IGenericRepository<Card>
    {
        public Task<bool> UpdateCardStatus(Card entry);
    }
}