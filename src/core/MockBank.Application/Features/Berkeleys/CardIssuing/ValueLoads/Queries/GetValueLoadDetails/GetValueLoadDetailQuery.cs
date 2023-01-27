using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MockBank.Application.Dto.Berkeley;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Application.Features.Berkeleys.CardIssuing.ValueLoads.Queries.GetValueLoadDetails
{
    public class GetValueLoadDetailQuery: IRequest<BKLoadFundsResponse>
    {
        public int transactionId { get; set; }
    }

    
    public class GetValueLoadDetailQueryHandler : IRequestHandler<GetValueLoadDetailQuery, BKLoadFundsResponse>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        
        public GetValueLoadDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BKLoadFundsResponse> Handle(GetValueLoadDetailQuery request, CancellationToken cancellationToken)
        {
            Transaction transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(request.transactionId);
            var result = _mapper.Map<BKLoadFundsData>(transaction);
            return new BKLoadFundsResponse {data = result};
        }
    }
}