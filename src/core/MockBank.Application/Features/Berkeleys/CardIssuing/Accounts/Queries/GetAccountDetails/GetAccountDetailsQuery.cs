using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MockBank.Application.Dto.Berkeley;
using MockBank.Application.IRepository;
using Account = MockBank.Domain.Entities.Berkeleys.Account;

namespace MockBank.Application.Features.Berkeleys.CardIssuing.Accounts.Queries.GetAccountDetails
{
    public class GetAccountDetailsQuery : IRequest<BKAccount>
    {
        public int accountId { get; set; }
    }
    
    public class GetAccountDetailsQueryHandler : IRequestHandler<GetAccountDetailsQuery,BKAccount >
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetAccountDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BKAccount> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)
        {
           // throw new System.NotImplementedException();
           var account = await _unitOfWork.AccountRepository.GetByIdAsync(request.accountId);
           var transactions = await _unitOfWork.TransactionRepository.GetTransactionsByAccountId(request.accountId);
           account.transactions = transactions;
           var accountData = _mapper.Map<BKAccountData>(account);
           return new BKAccount {data = accountData};
        }
    }
}