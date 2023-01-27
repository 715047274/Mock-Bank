using System.Threading.Tasks;
using MockBank.Domain.Common;

namespace MockBank.Application.Configurations.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}