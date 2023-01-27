using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MockBank.Application.Common.Helpers;
using MockBank.Application.Dto.Berkeley;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Application.Features.Berkeleys.CardIssuing.ValueLoads.Commands.CreateValueLoad
{
    public class CreateValueLoadCommand: BKLoadFunds, IRequest<BKLoadFundsResponse>
    {
        
    }

    public class CreateValueLoadCommandHandler : IRequestHandler<CreateValueLoadCommand, BKLoadFundsResponse>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CreateValueLoadCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<BKLoadFundsResponse> Handle(CreateValueLoadCommand request, CancellationToken cancellationToken)
        {
            // throw new System.NotImplementedException();
            var processEvt = MockHelpers.GenerateLoadFundEvt(request.message, status:"COMPLETE");
            var processId = await _unitOfWork.ProcessorEventRepository.AddAsync(processEvt);
            
            var mockTransaction = MockHelpers.GenerateValueLoadTransaction(
                request.account_id,
                request.amount, 
                processId,
                request.external_tag,
                request.idempotency_key);
            
            var mockTransactionId = await _unitOfWork.TransactionRepository.AddAsync(mockTransaction);

            #region update Account Balance
            var mockAccount = await _unitOfWork.AccountRepository.GetByIdAsync(request.account_id);
            mockAccount.balance = MockHelpers.AccountBalanceCalculator(mockAccount.balance, request.amount);
            mockAccount.processor_reference_id = processEvt.reference_id;
            mockAccount.updated_at = DateTime.Now;
            var isChangedAccount = await _unitOfWork.AccountRepository.UpdateAccountBalance(mockAccount);
            #endregion
          

            _unitOfWork.Complete();

            Transaction transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(mockTransactionId);
            var result = _mapper.Map<BKLoadFundsData>(transaction);
            return new BKLoadFundsResponse {data = result};
          }
        
        
        
        
    }
}