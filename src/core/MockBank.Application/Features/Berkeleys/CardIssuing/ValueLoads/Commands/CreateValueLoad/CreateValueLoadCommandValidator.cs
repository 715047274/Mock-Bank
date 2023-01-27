using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MockBank.Application.IRepository;

namespace MockBank.Application.Features.Berkeleys.CardIssuing.ValueLoads.Commands.CreateValueLoad
{
    public class CreateValueLoadCommandValidator : AbstractValidator<CreateValueLoadCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly int TimeoutValue = 104000;

        public CreateValueLoadCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            // db transaction with MustAsync 
            RuleFor(v => v.amount).MustAsync( async (amount, CancellationToken) =>
            {
                // default value is 104 seconds (104 * 1000 milisec)
                // TODO use background thread for the task delay
                if (amount >= 1551 && amount <= 1590)
                {

                    await Task.Run(async () =>
                    {
                        await Task.Delay(TimeoutValue);
                    });
                    
                    Console.WriteLine("scenario between 1551 and 1590 with delay ");
                }

               return true;
            });
        }
    }
}