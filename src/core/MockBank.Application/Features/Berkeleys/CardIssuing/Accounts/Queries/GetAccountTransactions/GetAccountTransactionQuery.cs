using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MockBank.Application.Dto.Berkeley;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Application.Features.Berkeleys.CardIssuing.Accounts.Queries.GetAccountTransactions
{
    public class GetAccountTransactionQuery : IRequest<List<BKTransactionHistoryData>>
    {
        public int id { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
    }

    public class GetAccountTransactionQueryHandler : IRequestHandler<GetAccountTransactionQuery, List<BKTransactionHistoryData>>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
         public GetAccountTransactionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
         {
             _unitOfWork = unitOfWork;
             _mapper = mapper;
         }

        public async Task<List<BKTransactionHistoryData>> Handle(GetAccountTransactionQuery request, CancellationToken cancellationToken)
        {
            // throw new System.NotImplementedException();

            var transactionList = await _unitOfWork.TransactionRepository.GetTransactionsByAccountId(request.id,
                request.startDate, request.endDate, request.limit, request.offset);
            var result = _mapper.Map<List<BKTransactionHistoryData>>(transactionList);
            
            return result ?? new List<BKTransactionHistoryData>();

        }
    }
}