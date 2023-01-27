using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MockBank.Application.Dto.Berkeley;
using MockBank.Application.IRepository;

namespace MockBank.Application.Features.Berkeleys.CardIssuing.Accounts.Queries.GetAccountBalanceQuery
{
    public class GetAccountBalanceQuery : IRequest<BKAccountBalanceResponse>
    {
        public int accountId { get; set; }
    }

    public class GetAccountBalanceQueryHandler : IRequestHandler<GetAccountBalanceQuery, BKAccountBalanceResponse>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public GetAccountBalanceQueryHandler(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BKAccountBalanceResponse> Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.AccountRepository.GetAccountBalanceById(request.accountId);
            var accountBalanceData = _mapper.Map<BKAccountBalance>(account);
            return new BKAccountBalanceResponse {data = accountBalanceData};
        }
    }
    
}