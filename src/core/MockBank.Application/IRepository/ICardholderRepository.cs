using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Application.IRepository
{
    public interface ICardholderRepository: IGenericRepository<CardHolder>
    {
        // public Task<CardHolder> CreateCardHolder(EntityEntry entry);
        // public Task<CardHolder> GetCardHolderDetails(EntityEntry entry);
        //
        // public Task<List<CardHolder>> GetListCardHolders();
    }
}