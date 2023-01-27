using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MockBank.Application.Common.Exceptions;
using MockBank.Application.Dto.Berkeley;
using MockBank.Application.IRepository;

namespace MockBank.Application.Configurations.Features.Berkeleys.CardIssuing.Accounts.Commands.UpdateActivateCard
{
    public class ActivateCardCommand : BKActivateCard, IRequest<bool>
    {
    }

    public class ActivateCardCommandHandler : IRequestHandler<ActivateCardCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActivateCardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ActivateCardCommand request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(request.id);
            var mockCard = account.cards.Where(x => x.last_four_digits == request.last_four_digits && x.status_code == "not_active").FirstOrDefault();
            if (mockCard != null || account !=null)
            {
                account.status_code = "active";
                account.updated_at = DateTime.Now;
                mockCard.status_code = "active";
                mockCard.activation_date = DateTime.Now;
                mockCard.updated_at = DateTime.Now;
                var isUpdated = await _unitOfWork.CardRepository.UpdateCardStatus(mockCard);
                var isAccountUpdated = await _unitOfWork.AccountRepository.ActiveAccountStatus(account);
                _unitOfWork.Complete();
                if (isUpdated && isAccountUpdated) return true;
                else
                {
                    throw new NotFoundException();
                }     
                   
            }
            else
            {
                return false;
            }
        }
    }
}