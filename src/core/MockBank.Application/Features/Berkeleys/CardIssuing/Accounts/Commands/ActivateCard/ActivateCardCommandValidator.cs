using FluentValidation;
using MockBank.Application.IRepository;

namespace MockBank.Application.Configurations.Features.Berkeleys.CardIssuing.Accounts.Commands.UpdateActivateCard
{
    public class ActivateCardCommandValidator :AbstractValidator<ActivateCardCommand>
    {
        private IUnitOfWork _unitOfWork;

        public ActivateCardCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}