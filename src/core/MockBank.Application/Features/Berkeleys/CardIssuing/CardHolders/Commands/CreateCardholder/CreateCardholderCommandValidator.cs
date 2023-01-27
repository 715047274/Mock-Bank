using System;
using FluentValidation;
//using MockBank.Application.Common.Exceptions;
using MockBank.Application.IRepository;
using NotFoundException = MockBank.Application.Common.Exceptions.NotFoundException;
namespace MockBank.Application.Features.Berkeleys.CardIssuing.CardHolders.Commands.CreateCardholder
{
    public class CreateCardholderCommandValidator : AbstractValidator<CreateCardholderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;


        public CreateCardholderCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(v => v.program_id).Must(id =>
            {
                if (id != 211)
                {
                    throw new NotFoundException("2121212");
                }

                return true;
            });
            RuleFor(v=> v.address1).NotEmpty().WithMessage("testing ");
        }

    }
}